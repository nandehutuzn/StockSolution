using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zn.Core.StockModel
{
    /// <summary>
    /// 股票信息模型
    /// </summary>
    public class StockInfoModel
    {
        /// <summary>
        /// 股票代号
        /// </summary>
        [Key]
        public string Id { get; set; }

        /// <summary>
        /// 股票名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 所属板块
        /// </summary>
        [Display(Name = "所属板块")]
        public string Sector { get; set; }

        /// <summary>
        /// 公司所在地
        /// </summary>
        [Display(Name = "公司所在地")]
        public string Address { get; set; }

        /// <summary>
        /// 市盈率
        /// </summary>
        [Display(Name = "市盈率")]
        public double? PERatio { get; set; }

        /// <summary>
        /// 0 上证 ， 1 深证
        /// </summary>
        public string Type { get; set; }
    }
}
