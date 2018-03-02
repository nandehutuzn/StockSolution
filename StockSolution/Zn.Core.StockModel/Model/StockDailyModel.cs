using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zn.Core.StockModel
{
    /// <summary>
    /// 每日股票模型
    /// </summary>
    public class StockDailyModel
    {
        [Key]
        public int ID { get; set; }

        /// <summary>
        /// 股票代码
        /// </summary>
        [Display(Name = "股票代码")]
        public string StockID { get; set; }

        /// <summary>
        /// 股票名称
        /// </summary>
        [Display(Name = "股票名称")]
        public string StockName { get; set; }

        /// <summary>
        /// 交易日
        /// </summary>
        [Display(Name = "交易日")]
        public DateTime TradeTime { get; set; }

        /// <summary>
        /// 开盘价
        /// </summary>
        [Display(Name = "开盘价")]
        public double OpenPrice { get; set; }

        /// <summary>
        /// 收盘价
        /// </summary>
        [Display(Name = "收盘价")]
        public double ClosePrice { get; set; }

        /// <summary>
        /// 最高价
        /// </summary>
        [Display(Name = "最高价")]
        public double HighestPrice { get; set; }

        /// <summary>
        /// 最低价
        /// </summary>
        [Display(Name = "最低价")]
        public double LowestPrice { get; set; }

        /// <summary>
        /// 成交量
        /// </summary>
        [Display(Name = "成交量")]
        public double TradingVolume { get; set; }

        /// <summary>
        /// 涨跌幅
        /// </summary>
        [Display(Name = "涨跌幅%")]
        public double UpOrDownRatio { get; set; }
    }
}
