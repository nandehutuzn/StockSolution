using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zn.Core.StockModel
{
    /// <summary>
    /// 股票指数模型
    /// </summary>
    public class StockIndexModel
    {
        [Key]
        public int ID { get; set; }

        /// <summary>
        /// 指数代号
        /// </summary>
        [Display(Name = "指数代号")]
        public string IndexID { get; set; }

        /// <summary>
        /// 当前点数
        /// </summary>
        [Display(Name = "当前点数")]
        public double CurrentPoint { get; set; }

        /// <summary>
        /// 当前相对于前一个工作日的点数
        /// </summary>
        [Display(Name = "当前相对于前一个工作日的点数")]
        public double CurrentPointRelative { get; set; }

        /// <summary>
        /// 涨跌率
        /// </summary>
        [Display(Name = "涨跌率")]
        public double UpOrDownRatio { get; set; }

        /// <summary>
        /// 成交量 ，单位手
        /// </summary>
        [Display(Name="成交量")]
        public double TradingVolume { get; set; }

        /// <summary>
        /// 成交额，单位万元
        /// </summary>
        [Display(Name="成交额(万元)")]
        public double TradingPriceSum { get; set; }

        /// <summary>
        /// 交易日
        /// </summary>
        [Display(Name = "交易日")]
        public DateTime Date { get; set; }
    }
}
