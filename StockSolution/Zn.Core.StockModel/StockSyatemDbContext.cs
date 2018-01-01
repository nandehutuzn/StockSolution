using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Zn.Core.Tools;

namespace Zn.Core.StockModel
{
    public class StockSyatemDbContext : DbContext
    {
        private ILog _log = Logger.Current;

        private StockSyatemDbContext() { }

        private static StockSyatemDbContext _default;
        public static StockSyatemDbContext Default {
            get {
                if (_default == null)
                {
                    Interlocked.CompareExchange(ref _default, new StockSyatemDbContext(), null);
                }
                return _default;
            }
        }

        /// <summary>
        /// 每日股票模型
        /// </summary>
        public DbSet<StockDailyModel> DailyModel { get; set; }


        /// <summary>
        /// 股票指数模型
        /// </summary>
        public DbSet<StockIndexModel> IndexModel { get; set; }

        
        /// <summary>
        /// 实时股票模型
        /// </summary>
        public DbSet<StockRealtimeModel> RealtimeModel { get; set; }

    }
}
