using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Zn.Core.Tools;

namespace Zn.Core.StockModel
{
    public class DataBaseService : StockSyatemDbContext, IDataBaseService
    {
        private ILog _log = Logger.Current;

        private DataBaseService()
            : base("name=DataBaseService")
        { }

        private static DataBaseService _default;
        public static DataBaseService Default
        {
            get
            {
                if (_default == null)
                {
                    Interlocked.CompareExchange(ref _default, new DataBaseService(), null);
                }
                return _default;
            }
        }

        public override Task<int> SaveChangesAsync()
        {
            return base.SaveChangesAsync();
        }

        #region Fields

        private List<StockDailyModel> _lstDailyModel;
        private List<StockIndexModel> _lstIndexModel;
        private List<StockRealtimeModel> _lstRealtimeModel;
        private List<StockSectorEnumModel> _lstSectorEnumModel;
        private List<StockInfoModel> _lstInfoModel;
		 
	    #endregion

        #region IDataBaseService接口

        #region Property
        
        public List<StockDailyModel> DailyModels(bool needUpdate = false)
        {
            if (_lstDailyModel == null || needUpdate)
            {
                _lstDailyModel = DailyModel.ToList();
            }
            return _lstDailyModel;
        }

        public List<StockIndexModel> IndexModels(bool needUpdate = false)
        {
            if (_lstIndexModel == null || needUpdate)
            {
                _lstIndexModel = IndexModel.ToList();
            }
            return _lstIndexModel;
        }

        public List<StockRealtimeModel> RealtimeModels(bool needUpdate = false)
        {
            if (_lstRealtimeModel == null || needUpdate)
            {
                _lstRealtimeModel = RealtimeModel.ToList();
            }
            return _lstRealtimeModel;
        }

        public List<StockSectorEnumModel> SectorEnumModels(bool needUpdate = false)
        {
            if (_lstSectorEnumModel == null || needUpdate)
            {
                _lstSectorEnumModel = SectorEnumModel.ToList();
            }
            return _lstSectorEnumModel;
        }

        public List<StockInfoModel> StockInfoModels(bool needUpdate = false)
        {
            if (_lstInfoModel == null || needUpdate)
            {
                _lstInfoModel = StockInfoModel.ToList();
            }
            return _lstInfoModel;
        }

        #endregion

        public Task<int> InsertDailyModel(StockDailyModel model)
        {
            if (model == null)
                throw new ArgumentNullException("model");
            model.TradeTime = DateTime.Now.Date;
            DailyModel.Add(model);
            Entry(model).State = System.Data.Entity.EntityState.Added;
            return SaveChangesAsync();
        }

        public Task<int> InsertDailyModel(IList<StockDailyModel> models)
        {
            if (models == null)
                throw new ArgumentNullException("models");
            foreach (var model in models)
            {
                model.TradeTime = DateTime.Now.Date;
                DailyModel.Add(model);
                Entry(model).State = System.Data.Entity.EntityState.Added;
            }
            return SaveChangesAsync();
        }

        public Task<int> InsertIndexModel(StockIndexModel model)
        {
            if (model == null)
                throw new ArgumentNullException("model");
            model.Date = DateTime.Now.Date;
            IndexModel.Add(model);
            Entry(model).State = System.Data.Entity.EntityState.Added;
            return SaveChangesAsync();
        }

        public Task<int> InsertIndexModel(IList<StockIndexModel> models)
        {
            if (models == null)
                throw new ArgumentNullException("models");
            foreach (var model in models)
            {
                model.Date = DateTime.Now.Date;
                IndexModel.Add(model);
                Entry(model).State = System.Data.Entity.EntityState.Added;
            }
            return SaveChangesAsync();
        }

        public Task<int> InsertRealtimeModel(StockRealtimeModel model)
        {
            if (model == null)
                throw new ArgumentNullException("model");
            model.CurrentTime = DateTime.Now;
            model.Date = DateTime.Now.Date;
            RealtimeModel.Add(model);
            Entry(model).State = System.Data.Entity.EntityState.Added;
            return SaveChangesAsync();
        }

        public Task<int> InsertRealtimeModel(IList<StockRealtimeModel> models)
        {
            if (models == null)
                throw new ArgumentNullException("models");
            foreach (var model in models)
            {
                model.CurrentTime = DateTime.Now;
                model.Date = DateTime.Now.Date;
                RealtimeModel.Add(model);
                Entry(model).State = System.Data.Entity.EntityState.Added;
            }
            return SaveChangesAsync();
        }

        public Task<int> DeleteDailyModel(StockDailyModel model)
        {
            DailyModel.Remove(model);
            Entry(model).State = System.Data.Entity.EntityState.Deleted;
            return SaveChangesAsync();
        }

        public Task<int> DeleteDailyModel(IList<StockDailyModel> models)
        {
            foreach (var model in models)
            {
                DailyModel.Remove(model);
                Entry(model).State = System.Data.Entity.EntityState.Deleted;
            }
            return SaveChangesAsync();
        }

        public Task<int> DeleteIndexModel(StockIndexModel model)
        {
            IndexModel.Remove(model);
            Entry(model).State = System.Data.Entity.EntityState.Deleted;
            return SaveChangesAsync();
        }

        public Task<int> DeleteIndexModel(IList<StockIndexModel> models)
        {
            foreach (var model in models)
            {
                IndexModel.Remove(model);
                Entry(model).State = System.Data.Entity.EntityState.Deleted;
            }
            return SaveChangesAsync();
        }

        public Task<int> DeleteRealtimeModel(StockRealtimeModel model)
        {
            RealtimeModel.Remove(model);
            Entry(model).State = System.Data.Entity.EntityState.Deleted;
            return SaveChangesAsync();
        }

        public Task<int> DeleteRealtimeModel(IList<StockRealtimeModel> models)
        {
            foreach (var model in models)
            {
                RealtimeModel.Remove(model);
                Entry(model).State = System.Data.Entity.EntityState.Deleted;
            }
            return SaveChangesAsync();
        }

        #endregion

        public Task<int> InsertStockInfoModel(StockInfoModel model)
        {
            if (model == null)
                throw new ArgumentNullException("model");
            StockInfoModel.Add(model);
            Entry(model).State = System.Data.Entity.EntityState.Added;
            return SaveChangesAsync();
        }

        public Task<int> InsretStockInfoModel(IList<StockInfoModel> models)
        {
            foreach (var model in models)
            {
                StockInfoModel.Add(model);
                Entry(model).State = System.Data.Entity.EntityState.Added;
            }
            return SaveChangesAsync();
        }

        public Task<int> InsertSectorEnumModel(StockSectorEnumModel model)
        {
            if (model == null)
                throw new ArgumentNullException("model");
            SectorEnumModel.Add(model);
            Entry(model).State = System.Data.Entity.EntityState.Added;
            return SaveChangesAsync();
        }

        public Task<int> InsertSectorEnumModel(IList<StockSectorEnumModel> models)
        {
            foreach (var model in models)
            {
                SectorEnumModel.Add(model);
                Entry(model).State = System.Data.Entity.EntityState.Added;
            }
            return SaveChangesAsync();
        }

        public Task<int> DeleteStockInfoModel(StockInfoModel model)
        {
            StockInfoModel.Remove(model);
            Entry(model).State = System.Data.Entity.EntityState.Deleted;
            return SaveChangesAsync();
        }

        public Task<int> DeleteStockInfoModel(IList<StockInfoModel> models)
        {
            foreach (var model in models)
            {
                StockInfoModel.Remove(model);
                Entry(model).State = System.Data.Entity.EntityState.Deleted;
            }
            return SaveChangesAsync();
        }

        public Task<int> DeleteStockSectorEnumModel(StockSectorEnumModel model)
        {
            SectorEnumModel.Remove(model);
            Entry(model).State = System.Data.Entity.EntityState.Deleted;
            return SaveChangesAsync();
        }

        public Task<int> DeleteStoclSectorEnumMpdel(IList<StockSectorEnumModel> models)
        {
            foreach (var model in models)
            {
                SectorEnumModel.Remove(model);
                Entry(model).State = System.Data.Entity.EntityState.Deleted;
            }
            return SaveChangesAsync();
        }
    }
}
