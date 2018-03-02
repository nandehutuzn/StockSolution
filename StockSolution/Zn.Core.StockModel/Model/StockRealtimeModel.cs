using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zn.Core.StockModel
{
    /// <summary>
    /// 实时股票模型
    /// </summary>
    public class StockRealtimeModel
    {
        [Key]
        public int ID { get; set; }

        /// <summary>
        /// 股票代码
        /// </summary>
        [Display(Name = "股票代码")]
        public string StockID { get; set; }

        /// <summary>
        /// 今日开盘价
        /// </summary>
        [Display(Name = "今日开盘价")]
        public double OpenPrice { get; set; }

        /// <summary>
        /// 昨日收盘价
        /// </summary>
        [Display(Name="昨日收盘价")]
        public double ClosePriceYesterday { get; set; }

        /// <summary>
        /// 当前价格
        /// </summary>
        [Display(Name = "当前价格")]
        public double NowPrice { get; set; }

        /// <summary>
        /// 今日最高价
        /// </summary>
        [Display(Name = "今日最高价")]
        public double HighestPriceToday { get; set; }

        /// <summary>
        /// 今日最低价
        /// </summary>
        [Display(Name = "今日最低价")]
        public double LowestPriceToday { get; set; }

        /// <summary>
        /// 竞买价，即买一的价格
        /// </summary>
        [Display(Name = "竞买价")]
        public double CompeteBuy { get; set; }

        /// <summary>
        /// 竞卖价，即卖一的价格
        /// </summary>
        [Display(Name = "竞卖价")]
        public double CompeteSell { get; set; }

        /// <summary>
        /// 成交量，单位为股票数
        /// </summary>
        [Display(Name = "成交量")]
        public int TradingVolume { get; set; }

        /// <summary>
        /// 成交金额 ，单位 元
        /// </summary>
        [Display(Name="成交金额")]
        public Int64 TradingSum { get; set; }

        /// <summary>
        /// 买一股票数量
        /// </summary>
        [Display(Name = "买一股票数量")]
        public int AmountBuy1 { get; set; }

        /// <summary>
        /// 买一报价
        /// </summary>
        [Display(Name = "买一报价")]
        public double PriceBuy1 { get; set; }

        /// <summary>
        /// 买二股票数量
        /// </summary>
        [Display(Name = "买二股票数量")]
        public int AmountBuy2 { get; set; }

        /// <summary>
        /// 买二报价
        /// </summary>
        [Display(Name = "买二报价")]
        public double PriceBuy2 { get; set; }

        /// <summary>
        /// 买三股票数量
        /// </summary>
        [Display(Name = "买三股票数量")]
        public int AmountBuy3 { get; set; }

        /// <summary>
        /// 买三报价
        /// </summary>
        [Display(Name = "买三报价")]
        public double PriceBuy3 { get; set; }

        /// <summary>
        /// 买四股票数量
        /// </summary>
        [Display(Name = "买四股票数量")]
        public int AmountBuy4 { get; set; }

        /// <summary>
        /// 买四报价
        /// </summary>
        [Display(Name = "买四报价")]
        public double PriceBuy4 { get; set; }

        /// <summary>
        /// 买五股票数量
        /// </summary>
        [Display(Name = "买五股票数量")]
        public int AmountBuy5 { get; set; }

        /// <summary>
        /// 买五报价
        /// </summary>
        [Display(Name = "买五报价")]
        public double PriceBuy5 { get; set; }

        /// <summary>
        /// 卖一股票数量
        /// </summary>
        [Display(Name = "卖一股票数量")]
        public int AmountSell1 { get; set; }

        /// <summary>
        /// 卖一报价
        /// </summary>
        [Display(Name = "卖一报价")]
        public double PriceSell1 { get; set; }

        /// <summary>
        /// 卖二股票数量
        /// </summary>
        [Display(Name = "卖二股票数量")]
        public int AmountSell2 { get; set; }

        /// <summary>
        /// 卖二报价
        /// </summary>
        [Display(Name = "卖二报价")]
        public double PriceSell2 { get; set; }

        /// <summary>
        /// 卖三股票数量
        /// </summary>
        [Display(Name = "卖三股票数量")]
        public int AmountSell3 { get; set; }

        /// <summary>
        /// 卖三报价
        /// </summary>
        [Display(Name = "卖三报价")]
        public double PriceSell3 { get; set; }

        /// <summary>
        /// 卖四股票数量
        /// </summary>
        [Display(Name = "卖四股票数量")]
        public int AmountSell4 { get; set; }

        /// <summary>
        /// 卖四报价
        /// </summary>
        [Display(Name = "卖四报价")]
        public double PriceSell4 { get; set; }

        /// <summary>
        /// 卖五股票数量
        /// </summary>
        [Display(Name = "卖五股票数量")]
        public int AmountSell5 { get; set; }

        /// <summary>
        /// 卖五报价
        /// </summary>
        [Display(Name = "卖五报价")]
        public double PriceSell5 { get; set; }

        /// <summary>
        /// 日期
        /// </summary>
        [Display(Name = "日期")]
        public DateTime Date { get; set; }

        /// <summary>
        /// 当前时间
        /// </summary>
        [Display(Name = "当前时间")]
        public DateTime CurrentTime { get; set; }
    }
}
