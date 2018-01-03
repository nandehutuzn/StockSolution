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
    public abstract class StockSyatemDbContext : DbContext
    {
        protected StockSyatemDbContext(string str)
            : base(str)
        { 
        
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

        /// <summary>
        /// 股票基本信息模型
        /// </summary>
        public DbSet<StockInfoModel> StockInfoModel { get; set; }

        /// <summary>
        /// 股票板块模型
        /// </summary>
        public DbSet<StockSectorEnumModel> SectorEnumModel { get; set; }
    }
}
