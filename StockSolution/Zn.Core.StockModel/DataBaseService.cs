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

        /// <summary>
        /// 插入单条数据
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        public Task<int> Insert<TEntity>(TEntity model) where TEntity : class
        {
            if (model == null)
                throw new ArgumentNullException("model");
            Set<TEntity>().Add(model);
            Entry(model).State = System.Data.Entity.EntityState.Added;
            return SaveChangesAsync();
        }

        /// <summary>
        /// 插入多条数据
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="models"></param>
        /// <returns></returns>
        public Task<int> Insert<TEntity>(IList<TEntity> models) where TEntity : class
        {
            if (models == null)
                throw new ArgumentNullException("models");
            foreach (var model in models)
            {
                Set<TEntity>().Add(model);
                Entry(model).State = System.Data.Entity.EntityState.Added;
            }
            return SaveChangesAsync();
        }


        /// <summary>
        /// 删除单条数据
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        public Task<int> Delete<TEntity>(TEntity model) where TEntity : class
        {
            Set<TEntity>().Remove(model);
            Entry(model).State = System.Data.Entity.EntityState.Deleted;
            return SaveChangesAsync();
        }

        /// <summary>
        /// 删除多条数据
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="models"></param>
        /// <returns></returns>
        public Task<int> Delete<TEntity>(IList<TEntity> models) where TEntity : class
        {
            foreach (var model in models)
            {
                Set<TEntity>().Remove(model);
                Entry(model).State = System.Data.Entity.EntityState.Deleted;
            }
            return SaveChangesAsync();
        }

        #endregion

    }
}
