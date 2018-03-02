using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zn.Core.StockModel
{
    /// <summary>
    /// 操作数据库接口
    /// </summary>
    public interface IDataBaseService
    {
        List<StockDailyModel> DailyModels(bool needUpdate = false);

        List<StockIndexModel> IndexModels(bool needUpdate = false);

        List<StockRealtimeModel> RealtimeModels(bool needUpdate = false);

        List<StockSectorEnumModel> SectorEnumModels(bool needUpdate = false);

        List<StockInfoModel> StockInfoModels(bool needUpdate = false);

        /// <summary>
        /// 插入单条数据
        /// </summary>
        /// <typeparam name="TEnity"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<int> Insert<TEnity>(TEnity model) where TEnity : class;

        /// <summary>
        /// 插入多条数据
        /// </summary>
        /// <typeparam name="TEnity"></typeparam>
        /// <param name="models"></param>
        /// <returns></returns>
        Task<int> Insert<TEnity>(IList<TEnity> models) where TEnity : class;

        /// <summary>
        /// 删除单条数据
        /// </summary>
        /// <typeparam name="TEnity"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<int> Delete<TEnity>(TEnity model) where TEnity : class;

        /// <summary>
        /// 删除多条数据
        /// </summary>
        /// <typeparam name="TEnity"></typeparam>
        /// <param name="models"></param>
        /// <returns></returns>
        Task<int> Delete<TEnity>(IList<TEnity> models) where TEnity : class;

    }
}
