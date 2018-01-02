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

        private DataBaseService() { }

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

        #region IDataBaseService接口

        #region Property
        
        public List<StockDailyModel> DailyModels
        {
            get
            {
                return _default.DailyModel.ToList();
            }
        }

        public List<StockIndexModel> IndexModels
        {
            get
            {
                return _default.IndexModel.ToList();
            }
        }

        public List<StockRealtimeModel> RealtimeModels
        {
            get
            {
                return _default.RealtimeModel.ToList();
            }
        }

        public List<StockSectorEnumModel> SectorEnumModels
        {
            get { return _default.SectorEnumModel.ToList(); }
        }

        public List<StockInfoModel> StockInfoModels
        {
            get { return _default.StockInfoModel.ToList(); }
        }

        #endregion

        public Task<int> InsertDailyModel(StockDailyModel model)
        {
            if (model == null)
                throw new ArgumentNullException("model");
            model.TradeTime = DateTime.Now.Date;
            _default.DailyModel.Add(model);
            _default.Entry(model).State = System.Data.Entity.EntityState.Added;
            return _default.SaveChangesAsync();
        }

        public Task<int> InsertDailyModel(IList<StockDailyModel> models)
        {
            if (models == null)
                throw new ArgumentNullException("models");
            foreach (var model in models)
            {
                model.TradeTime = DateTime.Now.Date;
                _default.DailyModel.Add(model);
                _default.Entry(model).State = System.Data.Entity.EntityState.Added;
            }
            return _default.SaveChangesAsync();
        }

        public Task<int> InsertIndexModel(StockIndexModel model)
        {
            if (model == null)
                throw new ArgumentNullException("model");
            model.Date = DateTime.Now.Date;
            _default.IndexModel.Add(model);
            _default.Entry(model).State = System.Data.Entity.EntityState.Added;
            return _default.SaveChangesAsync();
        }

        public Task<int> InsertIndexModel(IList<StockIndexModel> models)
        {
            if (models == null)
                throw new ArgumentNullException("models");
            foreach (var model in models)
            {
                model.Date = DateTime.Now.Date;
                _default.IndexModel.Add(model);
                _default.Entry(model).State = System.Data.Entity.EntityState.Added;
            }
            return _default.SaveChangesAsync();
        }

        public Task<int> InsertRealtimeModel(StockRealtimeModel model)
        {
            if (model == null)
                throw new ArgumentNullException("model");
            model.CurrentTime = DateTime.Now;
            model.Date = DateTime.Now.Date;
            _default.RealtimeModel.Add(model);
            _default.Entry(model).State = System.Data.Entity.EntityState.Added;
            return _default.SaveChangesAsync();
        }

        public Task<int> InsertRealtimeModel(IList<StockRealtimeModel> models)
        {
            if (models == null)
                throw new ArgumentNullException("models");
            foreach (var model in models)
            {
                model.CurrentTime = DateTime.Now;
                model.Date = DateTime.Now.Date;
                _default.RealtimeModel.Add(model);
                _default.Entry(model).State = System.Data.Entity.EntityState.Added;
            }
            return _default.SaveChangesAsync();
        }

        public Task<int> DeleteDailyModel(StockDailyModel model)
        {
            _default.DailyModel.Remove(model);
            _default.Entry(model).State = System.Data.Entity.EntityState.Deleted;
            return _default.SaveChangesAsync();
        }

        public Task<int> DeleteDailyModel(IList<StockDailyModel> models)
        {
            foreach (var model in models)
            {
                _default.DailyModel.Remove(model);
                _default.Entry(model).State = System.Data.Entity.EntityState.Deleted;
            }
            return _default.SaveChangesAsync();
        }

        public Task<int> DeleteIndexModel(StockIndexModel model)
        {
            _default.IndexModel.Remove(model);
            _default.Entry(model).State = System.Data.Entity.EntityState.Deleted;
            return _default.SaveChangesAsync();
        }

        public Task<int> DeleteIndexModel(IList<StockIndexModel> models)
        {
            foreach (var model in models)
            {
                _default.IndexModel.Remove(model);
                _default.Entry(model).State = System.Data.Entity.EntityState.Deleted;
            }
            return _default.SaveChangesAsync();
        }

        public Task<int> DeleteRealtimeModel(StockRealtimeModel model)
        {
            _default.RealtimeModel.Remove(model);
            _default.Entry(model).State = System.Data.Entity.EntityState.Deleted;
            return _default.SaveChangesAsync();
        }

        public Task<int> DeleteRealtimeModel(IList<StockRealtimeModel> models)
        {
            foreach (var model in models)
            {
                _default.RealtimeModel.Remove(model);
                _default.Entry(model).State = System.Data.Entity.EntityState.Deleted;
            }
            return _default.SaveChangesAsync();
        }

        #endregion

        public Task<int> InsertStockInfoModel(StockInfoModel model)
        {
            if (model == null)
                throw new ArgumentNullException("model");
            _default.StockInfoModel.Add(model);
            _default.Entry(model).State = System.Data.Entity.EntityState.Added;
            return _default.SaveChangesAsync();
        }

        public Task<int> InsretStockInfoModel(IList<StockInfoModel> models)
        {
            foreach (var model in models)
            {
                _default.StockInfoModel.Add(model);
                _default.Entry(model).State = System.Data.Entity.EntityState.Added;
            }
            return _default.SaveChangesAsync();
        }

        public Task<int> InsertSectorEnumModel(StockSectorEnumModel model)
        {
            if (model == null)
                throw new ArgumentNullException("model");
            _default.SectorEnumModel.Add(model);
            _default.Entry(model).State = System.Data.Entity.EntityState.Added;
            return _default.SaveChangesAsync();
        }

        public Task<int> InsertSectorEnumModel(IList<StockSectorEnumModel> models)
        {
            foreach (var model in models)
            {
                _default.SectorEnumModel.Add(model);
                _default.Entry(model).State = System.Data.Entity.EntityState.Added;
            }
            return _default.SaveChangesAsync();
        }

        public Task<int> DeleteStockInfoModel(StockInfoModel model)
        {
            _default.StockInfoModel.Remove(model);
            _default.Entry(model).State = System.Data.Entity.EntityState.Deleted;
            return _default.SaveChangesAsync();
        }

        public Task<int> DeleteStockInfoModel(IList<StockInfoModel> models)
        {
            foreach (var model in models)
            {
                _default.StockInfoModel.Remove(model);
                _default.Entry(model).State = System.Data.Entity.EntityState.Deleted;
            }
            return _default.SaveChangesAsync();
        }

        public Task<int> DeleteStockSectorEnumModel(StockSectorEnumModel model)
        {
            _default.SectorEnumModel.Remove(model);
            _default.Entry(model).State = System.Data.Entity.EntityState.Deleted;
            return _default.SaveChangesAsync();
        }

        public Task<int> DeleteStoclSectorEnumMpdel(IList<StockSectorEnumModel> models)
        {
            foreach (var model in models)
            {
                _default.SectorEnumModel.Remove(model);
                _default.Entry(model).State = System.Data.Entity.EntityState.Deleted;
            }
            return _default.SaveChangesAsync();
        }
    }
}
