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
                        models.ForEach(async o => await AddDailyModel(DateTime.Now.ToString("yyyy-MM-dd"), o.Id, o.Name, o.Type));
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
        public async Task AddDailyModel(string date, string stockId, string stockName, string type)        
        {
            try
            {
                string url = string.Format(ConstValue.LIANGYEEURL, date, stockId, date, type);
                var result = await _httpService.GetDailyModel(url);
                if (result != null)
                {
                    await _log.Info(string.Format("股票{0} 获取 {1} 信息成功", stockName, date));
                    result.StockID = stockId;
                    result.StockName = stockName;
                    await _dataService.InsertDailyModel(result);
                }
            }
            catch (Exception ex)
            {
                _log.Error(ex);
            }
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
        public async Task AddDailyModels(string dateStart, string dateEnd, string stockId, string stockName, string type)
        {
            string url = string.Format(ConstValue.LIANGYEEURL, dateStart, stockId, dateEnd, type);
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
        }

        public Task AddIndexModel(string indexId, string type)
        {
            throw new NotImplementedException();
        }

        public Task AddRealtimeModel(string stockId, string type)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
