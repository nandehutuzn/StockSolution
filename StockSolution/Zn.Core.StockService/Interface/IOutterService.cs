using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zn.Core.StockModel;

namespace Zn.Core.StockService
{
    /// <summary>
    /// 对外服务的接口
    /// </summary>
    public interface IOutterService
    {
        /// <summary>
        /// 启动
        /// </summary>
        /// <returns></returns>
        void Start();

        /// <summary>
        /// 终止
        /// </summary>
        /// <returns></returns>
        void Stop();

        /// <summary>
        /// 增加某支股票每日数据
        /// </summary>
        /// <param name="date"></param>
        /// <param name="stockId"></param>
        /// <param name="stockName"></param>
        /// <param name="type">0 上证，1 深证</param>
        /// <returns></returns>
        Task<StockDailyModel> AddDailyModel(string date, string stockId, string stockName, string type);

        /// <summary>
        /// 增加某支股票某个时间段的数据
        /// </summary>
        /// <param name="dateStart"></param>
        /// <param name="stockId"></param>
        /// <param name="stockName"></param>
        /// <param name="type">0 上证，1 深证</param>
        /// <returns></returns>
        Task<List<StockDailyModel>> AddDailyModels(string dateStart, string  dateEnd, string stockId, string stockName, string type);

        /// <summary>
        /// 增加某个指数每日数据
        /// </summary>
        /// <param name="indexId"></param>
        /// <param name="type">sh 上证 ， sz 深证</param>
        /// <returns></returns>
        Task<StockIndexModel> AddIndexModel(string indexId, string type);

        /// <summary>
        /// 增加某支股票实时数据
        /// </summary>
        /// <param name="stockId"></param>
        /// <param name="type">sh 上证 ， sz 深证</param>
        /// <returns></returns>
        Task<StockRealtimeModel> AddRealtimeModel(string stockId, string type);
    }
}
