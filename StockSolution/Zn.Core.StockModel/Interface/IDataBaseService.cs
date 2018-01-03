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

        Task<int> InsertDailyModel(StockDailyModel model);

        Task<int> InsertDailyModel(IList<StockDailyModel> models);

        Task<int> InsertIndexModel(StockIndexModel model);

        Task<int> InsertIndexModel(IList<StockIndexModel> models);

        Task<int> InsertRealtimeModel(StockRealtimeModel model);

        Task<int> InsertRealtimeModel(IList<StockRealtimeModel> models);

        Task<int> InsertStockInfoModel(StockInfoModel model);

        Task<int> InsretStockInfoModel(IList<StockInfoModel> models);

        Task<int> InsertSectorEnumModel(StockSectorEnumModel model);

        Task<int> InsertSectorEnumModel(IList<StockSectorEnumModel> models);

        Task<int> DeleteDailyModel(StockDailyModel model);

        Task<int> DeleteDailyModel(IList<StockDailyModel> models);

        Task<int> DeleteIndexModel(StockIndexModel model);

        Task<int> DeleteIndexModel(IList<StockIndexModel> models);

        Task<int> DeleteRealtimeModel(StockRealtimeModel model);

        Task<int> DeleteRealtimeModel(IList<StockRealtimeModel> models);

        Task<int> DeleteStockInfoModel(StockInfoModel model);

        Task<int> DeleteStockInfoModel(IList<StockInfoModel> models);

        Task<int> DeleteStockSectorEnumModel(StockSectorEnumModel model);

        Task<int> DeleteStoclSectorEnumMpdel(IList<StockSectorEnumModel> models);
    }
}
