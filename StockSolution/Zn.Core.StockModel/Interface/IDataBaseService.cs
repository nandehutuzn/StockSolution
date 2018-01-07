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

        Task<int> Insert<TEnity>(TEnity model) where TEnity : class;

        Task<int> Insert<TEnity>(IList<TEnity> models) where TEnity : class;


        Task<int> Delete<TEnity>(TEnity model) where TEnity : class;

        Task<int> Delete<TEnity>(IList<TEnity> models) where TEnity : class;

    }
}
