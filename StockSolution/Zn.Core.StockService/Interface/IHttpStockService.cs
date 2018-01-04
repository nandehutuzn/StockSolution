using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zn.Core.StockModel;

namespace Zn.Core.StockService
{
    /// <summary>
    /// 通过http获取股票信息接口
    /// </summary>
    interface IHttpStockService
    {
        /// <summary>
        /// 获取某支股票每日数据
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        Task<StockDailyModel> GetDailyModel(string url);

        /// <summary>
        /// 获取某支股票某个时间段的数据
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        Task<List<StockDailyModel>> GetDailyModels(string url);

        /// <summary>
        /// 获取某个指数每日数据
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        Task<StockIndexModel> GetIndexModel(string url);

        /// <summary>
        /// 获取某支股票实时数据
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        Task<StockRealtimeModel> GetRealtimeModel(string url);
    }
}
