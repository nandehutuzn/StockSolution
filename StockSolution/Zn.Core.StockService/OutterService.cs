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

        #region Interface

        public async Task Start()
        {
            await Task.Run(() =>
                {
                    try
                    {
                        var models = _dataService.StockInfoModels(); //从数据库中获取所有股票模型
                        DateTime lastWorkDay = DateTime.Now;
                        if (DateTime.Now.DayOfWeek == DayOfWeek.Saturday)
                            lastWorkDay = DateTime.Now.AddDays(-1);
                        else if (DateTime.Now.DayOfWeek == DayOfWeek.Sunday)
                            lastWorkDay = DateTime.Now.AddDays(-2);
                        //models.ForEach(async o => await AddDailyModel(lastWorkDay.ToString("yyyy-MM-dd"), o.Id, o.Name, o.Type));
                        //models.ForEach(async o => await AddDailyModels(lastWorkDay.AddDays(-1000).ToString("yyyy-MM-dd"), lastWorkDay.AddDays(-361).ToString("yyyy-MM-dd"), o.Id, o.Name, o.Type));
                        models.ForEach(async o => await AddRealtimeModel(o.Id, o.Type));
                    }
                    catch (Exception ex)
                    {
                        _log.Error(ex);
                    }
                });
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
                    await _log.Info(string.Format("股票{0} 获取 {1} 信息成功", stockName, date));
                    result.StockID = stockId;
                    result.StockName = stockName;
                    await _dataService.InsertDailyModel(result);

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
                await _log.Info(string.Format("股票{0} 获取 {1}---{2} 信息成功", stockName, dateStart, dateEnd));
                result.ForEach(o =>
                    {
                        o.StockID = stockId;
                        o.StockName = stockName;
                    });
                await _dataService.InsertDailyModel(result);
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
                await _log.Info(string.Format("获取指数 {0} 实时信息成功", indexId));
                result.IndexID = indexId;
                await _dataService.InsertIndexModel(result);
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
            _autoResetEventSina.WaitOne();
            Thread.Sleep(500);
            var result = await _httpService.GetRealtimeModel(url);
            if (result != null)
            {
                await _log.Info(string.Format("获取股票 {0} 实时信息成功", stockId));
                result.StockID = stockId;
                await _dataService.InsertRealtimeModel(result);

            }
            _autoResetEventSina.Set();
            return result;
        }

        #endregion
    }
}
