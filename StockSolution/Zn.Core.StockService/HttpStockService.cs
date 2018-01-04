using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Zn.Core.Tools;
using Zn.Core.StockModel;
using Newtonsoft.Json;

namespace Zn.Core.StockService
{
    /// <summary>
    /// http获取股票信息服务
    /// </summary>
    public class HttpStockService : IHttpStockService
    {
        #region Fields

        private static HttpStockService _default;
        private ILog _log = Logger.Current;
        private IDataBaseService _dataService = DataBaseService.Default;

        #endregion

        #region Constructor

        private HttpStockService() { }

        #endregion

        #region Properties

        public static HttpStockService Default {
            get {
                if (_default == null)
                {
                    Interlocked.CompareExchange(ref _default, new HttpStockService(), null);
                }
                return _default;
            }
        }

        #endregion


        #region Interface

        /// <summary>
        /// 获取某支股票单日的信息
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public async Task<StockModel.StockDailyModel> GetDailyModel(string url)
        {
            if (string.IsNullOrEmpty(url))
                throw new ArgumentNullException("url");
            string result = await HttpHelper.GetHttpString(url, Encoding.UTF8);
            if (string.IsNullOrEmpty(result))
                return null;
            try
            {
                var tmp = JsonConvert.DeserializeObject<LiangYeeHttpRespHelper>(result);
                if (tmp != null)
                {
                    _log.Info(string.Format("url:{0} \n Code:{1} \n Message: {2}", url, tmp.Code, tmp.Message));
                    return ConvertToDailyModel(tmp.Result[0]); //只获取单日的
                }
                return null;
            }
            catch (Exception ex)
            {
                _log.Error(string.Format("url: {0}", url), ex);
                return null;
            }
        }

        public async Task<List<StockDailyModel>> GetDailyModels(string url)
        {
            if (string.IsNullOrEmpty(url))
                throw new ArgumentNullException("url");
            string result = await HttpHelper.GetHttpString(url, Encoding.UTF8);
            if (string.IsNullOrEmpty(result))
                return null;
            try
            {
                var tmp = JsonConvert.DeserializeObject<LiangYeeHttpRespHelper>(result);
                if (tmp != null)
                {
                    _log.Info(string.Format("url:{0} \n Code:{1} \n Message: {2}", url, tmp.Code, tmp.Message));
                    List<StockDailyModel> lstModel = new List<StockDailyModel>();
                    foreach (string dataLine in tmp.Result)
                    {
                        lstModel.Add(ConvertToDailyModel(dataLine));
                    }
                    return lstModel;
                }
                return null;
            }
            catch (Exception ex)
            {
                _log.Error(string.Format("url: {0}", url), ex);
                return null;
            }
        }


        public Task<StockModel.StockIndexModel> GetIndexModel(string url)
        {
            throw new NotImplementedException();
        }

        public Task<StockModel.StockRealtimeModel> GetRealtimeModel(string url)
        {
            throw new NotImplementedException();
        }


        #endregion

        #region Func

        /// <summary>
        /// LiangYee返回数据转换为实体
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private StockDailyModel ConvertToDailyModel(string data)
        {
            if (data != null)
            {
                DateTime datetime;
                double val;
                StockDailyModel model = new StockDailyModel();
                string[] tmpStr = data.Split(',');
                DateTime.TryParse(tmpStr[0], out datetime);
                model.TradeTime = datetime;
                double.TryParse(tmpStr[1], out val);
                model.OpenPrice = val;
                double.TryParse(tmpStr[2], out val);
                model.ClosePrice = val;
                double.TryParse(tmpStr[3], out val);
                model.HighestPrice = val;
                double.TryParse(tmpStr[4], out val);
                model.LowestPrice = val;
                double.TryParse(tmpStr[5], out val);
                model.TradingVolume = val;
                double.TryParse(tmpStr[6], out val);
                model.UpOrDownRatio = val;

                return model;
            }
            return null;
        }

        #endregion

    }

    /// <summary>
    /// LiangYee 返回数据解析帮助类
    /// </summary>
    class LiangYeeHttpRespHelper
    {
        public string Code { get; set; }

        public string Message { get; set; }

        public string[] Result { get; set; }
    }
}
