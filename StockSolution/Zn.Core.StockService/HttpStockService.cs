using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Zn.Core.Tools;
using Zn.Core.StockModel;
using Newtonsoft.Json;
using System.Threading;

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
                if (tmp != null && tmp.Result != null && tmp.Result.Length > 0)
                {
                    _log.Info(string.Format("url:{0} \n Code:{1} \n Message: {2}", url, tmp.Code, tmp.Message));
                    return ConvertToDailyModel(tmp.Result[0]); //只获取单日的
                }
                else if (tmp != null)
                {
                    await MessageManager.NotifyMessage(MessageKey.OPERATEMESSAGE, string.Format("请求失败 URL: {0}, Message: ", url, tmp.Message));
                    await _log.Info(string.Format("请求失败 URL: {0}, Message: ", url, tmp.Message));
                }
                return null;
            }
            catch (Exception ex)
            {
                _log.Error(string.Format("url: {0}", url), ex);
                return null;
            }
        }

        /// <summary>
        /// 获取多支股票每日信息
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
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
                if (tmp != null && tmp.Result != null && tmp.Result.Length > 0)
                {
                    _log.Info(string.Format("url:{0} \n Code:{1} \n Message: {2}", url, tmp.Code, tmp.Message));
                    List<StockDailyModel> lstModel = new List<StockDailyModel>();
                    foreach (string dataLine in tmp.Result)
                    {
                        lstModel.Add(ConvertToDailyModel(dataLine));
                    }
                    return lstModel;
                }
                else if (tmp != null)
                {
                    await MessageManager.NotifyMessage(MessageKey.OPERATEMESSAGE, string.Format("请求失败 URL: {0}, Message: ", url, tmp.Message));
                    await _log.Info(string.Format("请求失败 URL: {0}, Message: ", url, tmp.Message));
                }
                return null;
            }
            catch (Exception ex)
            {
                _log.Error(string.Format("url: {0}", url), ex);
                return null;
            }
        }

        /// <summary>
        /// 获取指数信息
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public async Task<StockModel.StockIndexModel> GetIndexModel(string url)
        {
            if (string.IsNullOrEmpty(url))
                throw new ArgumentNullException("url");
            string result = await HttpHelper.GetHttpString(url);
            if (string.IsNullOrEmpty(result))
            {
                await _log.Info(string.Format("获取指数信息失败, {0}", url));
                return null;
            }
            try
            {
                string[] data = result.Trim(';').Split(',');
                if (data.Length >= 6)
                {
                    StockIndexModel model = new StockIndexModel()
                    {
                        CurrentPoint = double.Parse(data[1]),
                        CurrentPointRelative = double.Parse(data[2]),
                        UpOrDownRatio = double.Parse(data[3]),
                        TradingVolume = double.Parse(data[4]),
                        TradingPriceSum = double.Parse(data[5]),
                    };
                    return model;
                }
                else
                    await _log.Info(string.Format("获取股指数信息数据不全，{0}", url));

            }
            catch (Exception ex)
            {
                _log.Error(ex);
            }
            return null;
        }

        /// <summary>
        /// 获取股票实时信息
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public async Task<StockModel.StockRealtimeModel> GetRealtimeModel(string url)
        {
            if (string.IsNullOrEmpty(url))
                throw new ArgumentNullException("url");
            string result = await HttpHelper.GetHttpString(url);
            if (string.IsNullOrEmpty(result))
            {
                await MessageManager.NotifyMessage(MessageKey.OPERATEMESSAGE, string.Format("获取股票实时信息失败，{0}", url));
                await _log.Info(string.Format("获取股票实时信息失败，{0}", url));
                return null;
            }
            try
            {
                string[] data = result.Trim(';').Split(',');
                if (data.Length >= 32)
                {
                    #region 生成model

                    StockRealtimeModel model = new StockRealtimeModel();
                    model.OpenPrice = double.Parse(data[1]);
                    model.ClosePriceYesterday = double.Parse(data[2]);
                    model.NowPrice = double.Parse(data[3]);
                    model.HighestPriceToday = double.Parse(data[4]);
                    model.LowestPriceToday = double.Parse(data[5]);
                    model.CompeteBuy = double.Parse(data[6]);
                    model.CompeteSell = double.Parse(data[7]);
                    model.TradingVolume = int.Parse(data[8]);
                    model.TradingSum = (Int64)double.Parse(data[9]);
                    model.AmountBuy1 = int.Parse(data[10]);
                    model.PriceBuy1 = double.Parse(data[11]);
                    model.AmountBuy2 = int.Parse(data[12]);
                    model.PriceBuy2 = double.Parse(data[13]);
                    model.AmountBuy3 = int.Parse(data[14]);
                    model.PriceBuy3 = double.Parse(data[15]);
                    model.AmountBuy4 = int.Parse(data[16]);
                    model.PriceBuy4 = double.Parse(data[17]);
                    model.AmountBuy5 = int.Parse(data[18]);
                    model.PriceBuy5 = double.Parse(data[19]);
                    model.AmountSell1 = int.Parse(data[20]);
                    model.PriceSell1 = double.Parse(data[21]);
                    model.AmountSell2 = int.Parse(data[22]);
                    model.PriceSell2 = double.Parse(data[23]);
                    model.AmountSell3 = int.Parse(data[24]);
                    model.PriceSell3 = double.Parse(data[25]);
                    model.AmountSell4 = int.Parse(data[26]);
                    model.PriceSell4 = double.Parse(data[27]);
                    model.AmountSell5 = int.Parse(data[28]);
                    model.PriceSell5 = double.Parse(data[29]);
                   
                    model.Date = Convert.ToDateTime(data[30]);
                    model.CurrentTime = Convert.ToDateTime(data[30] + " " + data[31]);

                    #endregion

                    return model;
                }
                await MessageManager.NotifyMessage(MessageKey.OPERATEMESSAGE, string.Format("获取股票实时信息数据不全，{0}，结果为{1}", url, result));
                await _log.Info(string.Format("获取股票实时信息数据不全，{0}", url));
            }
            catch (Exception ex)
            {
                _log.Error(ex);
            }
            return null;
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
