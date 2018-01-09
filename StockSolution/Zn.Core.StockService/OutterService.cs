using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Zn.Core.Tools;
using Zn.Core.StockModel;
using Zn.Core.StockService.Constant;

namespace Zn.Core.StockService
{
    /// <summary>
    /// 对外服务类
    /// </summary>
    public class OutterService : IOutterService
    {
        #region Fields

        private static OutterService _default;
        private ILog _log = Logger.Current;
        private IDataBaseService _dataService = DataBaseService.Default;
        private IHttpStockService _httpService = HttpStockService.Default;
        private AutoResetEvent _autoResetEventLiangYee = new AutoResetEvent(true);
        private AutoResetEvent _autoResetEventSina = new AutoResetEvent(true);
        private System.Timers.Timer _timer;

        #endregion

        #region Constructor

        private OutterService()
        {
            //注册有新股票加入消息通知
            MessageManager.Register(MessageKey.ADDSTOCKINFOMODE, AddStockInfoModeCallback);
            _timer = new System.Timers.Timer(1000 * 60 * 10); // 10分钟查一次
            _timer.Elapsed += async (s, e) => await Start();
            _timer.Start();
        }

        private async void AddStockInfoModeCallback(object obj)
        {
            List<string> lstInfo = obj as List<string>;
            if (lstInfo != null && lstInfo.Count == 3)
            {
                await AddDailyModels(DateTime.Now.AddDays(-360).ToString("yyyy-MM-dd"), DateTime.Now.ToString("yyyy-MM-dd"), lstInfo[0], lstInfo[1], lstInfo[2]);
            }
        }

        #endregion

        #region Properties

        public static OutterService Default {
            get {
                if (_default == null)
                {
                    Interlocked.CompareExchange(ref _default, new OutterService(), null);
                }
                return _default;
            }
        }

        #endregion

        #region Func

        /// <summary>
        /// 是否需要开始查询实时股票信息
        /// </summary>
        /// <returns></returns>
        private bool NeedQueryRealtimeStock()
        {
            try
            {
                string amWorkingTimeStart = "09:30";
                string amWorkingTimeEnd = "11:30";
                string pmWorkingTimeStart = "13:00";
                string pmWorkingTimeEnd = "15:00";

                TimeSpan amWorkingTsStart = DateTime.Parse(amWorkingTimeStart).TimeOfDay;
                TimeSpan amWorkingTsEnd = DateTime.Parse(amWorkingTimeEnd).TimeOfDay;
                TimeSpan pmWorkingTsStart = DateTime.Parse(pmWorkingTimeStart).TimeOfDay;
                TimeSpan pmWorkingTsEnd = DateTime.Parse(pmWorkingTimeEnd).TimeOfDay;

                var time = DateTime.Now.TimeOfDay;
                if (time >= amWorkingTsStart && time <= amWorkingTsEnd)
                    return true;
                else if (time >= pmWorkingTsStart && time <= pmWorkingTsEnd)
                    return true;
                return false;
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                return false;
            }
        }

        #endregion

        #region Interface

        public async Task Start()
        {
            await Task.Run(() =>
                {
                    try
                    {
                        MessageManager.NotifyMessage(MessageKey.OPERATEMESSAGE, string.Format("现在时间：{0}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
                        if (NeedQueryRealtimeStock())
                        {
                            var models = _dataService.StockInfoModels(); //从数据库中获取所有股票模型
                            MessageManager.NotifyMessage(MessageKey.OPERATEMESSAGE, string.Format("从数据库拿到 {0} 支股票信息", models.Count));
                            DateTime lastWorkDay = DateTime.Now;
                            if (DateTime.Now.DayOfWeek == DayOfWeek.Saturday)
                                lastWorkDay = DateTime.Now.AddDays(-1);
                            else if (DateTime.Now.DayOfWeek == DayOfWeek.Sunday)
                                lastWorkDay = DateTime.Now.AddDays(-2);
                            //models.ForEach(async o => await AddDailyModel(lastWorkDay.ToString("yyyy-MM-dd"), o.Id, o.Name, o.Type));
                            //models.ForEach(async o => await AddDailyModels(lastWorkDay.AddDays(-1000).ToString("yyyy-MM-dd"), lastWorkDay.AddDays(-361).ToString("yyyy-MM-dd"), o.Id, o.Name, o.Type));
                            models.ForEach(async o => await AddRealtimeModel(o.Id, o.Type));
                        }

                     
                    }
                    catch (Exception ex)
                    {
                        _log.Error(ex);
                    }
                });
        }

        /// <summary>
        /// 终止查询
        /// </summary>
        public void Stop()
        {
            if (_timer != null)
            {
                _timer.Stop();
                _timer.Close();
                _timer = null;
            }
        }


        /// <summary>
        /// 增加某支股票每日数据
        /// </summary>
        /// <param name="date"></param>
        /// <param name="stockId"></param>
        /// <param name="stockName"></param>
        /// <param name="type">0 上证，1 深证</param>
        /// <returns></returns>
        public async Task<StockDailyModel> AddDailyModel(string date, string stockId, string stockName, string type)        
        {
            try
            {
                string url = string.Format(ConstValue.LIANGYEEURL, date, stockId, date, type);
                _autoResetEventLiangYee.WaitOne(); //第三方服务不支持并发访问，所以在这里做控制
                Thread.Sleep(2000);
                var result = await _httpService.GetDailyModel(url);
                if (result != null)
                {
                    await MessageManager.NotifyMessage(MessageKey.OPERATEMESSAGE, string.Format("股票{0} 获取 {1} 信息成功", stockName, date));
                    await _log.Info(string.Format("股票{0} 获取 {1} 信息成功", stockName, date));
                    result.StockID = stockId;
                    result.StockName = stockName;
                    int i= await _dataService.Insert<StockDailyModel>(result);
                    string ret = i == 1 ? "成功" : "失败";
                    await MessageManager.NotifyMessage(MessageKey.OPERATEMESSAGE, string.Format("股票 {0}, {1} 数据写入数据库{2}", stockId, date, ret));
                }
                _autoResetEventLiangYee.Set();
                return result;
            }
            catch (Exception ex)
            {
                _log.Error(ex);
            }
            return null;
        }

        /// <summary>
        /// 增加某支股票某个时间段的数据
        /// </summary>
        /// <param name="dateStart"></param>
        /// <param name="dateEnd"></param>
        /// <param name="stockId"></param>
        /// <param name="stockName"></param>
        /// <param name="type">0 上证，1 深证</param>
        /// <returns></returns>
        public async Task<List<StockDailyModel>> AddDailyModels(string dateStart, string dateEnd, string stockId, string stockName, string type)
        {
            string url = string.Format(ConstValue.LIANGYEEURL, dateStart, stockId, dateEnd, type);
            _autoResetEventLiangYee.WaitOne(); //第三方服务不支持并发访问，所以在这里做控制
            Thread.Sleep(2000);
            var result = await _httpService.GetDailyModels(url);
            if (result != null)
            {
                await MessageManager.NotifyMessage(MessageKey.OPERATEMESSAGE, string.Format("股票{0} 获取 {1}---{2} 信息成功", stockName, dateStart, dateEnd));
                await _log.Info(string.Format("股票{0} 获取 {1}---{2} 信息成功", stockName, dateStart, dateEnd));
                result.ForEach(o =>
                    {
                        o.StockID = stockId;
                        o.StockName = stockName;
                    });
                int i = await _dataService.Insert<StockDailyModel>(result);
                string ret = i == result.Count ? "成功" : "失败";
                await MessageManager.NotifyMessage(MessageKey.OPERATEMESSAGE, string.Format("{0} 支股票  {1}, {2}----{3} 数据写入数据库{4}", result.Count, stockId, dateStart, dateEnd, ret));
            }

            _autoResetEventLiangYee.Set();
            return result;
        }

        /// <summary>
        /// 获取指数信息
        /// </summary>
        /// <param name="indexId"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public async Task<StockIndexModel> AddIndexModel(string indexId, string type)
        {
            if (type == "0")
                type = "sh";
            else if (type == "1")
                type = "sz";
            string url = string.Format(Constant.ConstValue.XINAREALTIMEURL, type, indexId);
            _autoResetEventSina.WaitOne();
            Thread.Sleep(500);
            var result = await _httpService.GetIndexModel(url);
            if (result != null)
            {
                await MessageManager.NotifyMessage(MessageKey.OPERATEMESSAGE, string.Format("获取指数 {0} 实时信息成功", indexId));
                await _log.Info(string.Format("获取指数 {0} 实时信息成功", indexId));
                result.IndexID = indexId;
                await _dataService.Insert<StockIndexModel>(result);
            }
            _autoResetEventSina.Set();
            return result;
        }

        /// <summary>
        /// 获取股票实时信息
        /// </summary>
        /// <param name="stockId"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public async Task<StockRealtimeModel> AddRealtimeModel(string stockId, string type)
        {
            if(type == "0")
                type = "sh";
            else if(type == "1")
                type = "sz";
            string url = string.Format(Constant.ConstValue.XINAREALTIMEURL, type, stockId);
            _autoResetEventSina.WaitOne(); //EF 不支持并发写入
            //Thread.Sleep(500);
            var result = await _httpService.GetRealtimeModel(url);
            if (result != null)
            {
                await MessageManager.NotifyMessage(MessageKey.OPERATEMESSAGE, string.Format("获取股票 {0} 实时信息成功", stockId));
                await _log.Info(string.Format("获取股票 {0} 实时信息成功", stockId));
                result.StockID = stockId;
                int i= await _dataService.Insert<StockRealtimeModel>(result);
                string ret = i == 1 ? "成功" : "失败";
                await MessageManager.NotifyMessage(MessageKey.OPERATEMESSAGE, string.Format("股票 {0}, {1} 实时数据写入数据库{2}", stockId, result.CurrentTime.ToString(), ret));
            }
            _autoResetEventSina.Set();
            return result;
        }

        #endregion
    }
}
