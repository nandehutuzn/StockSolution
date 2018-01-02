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
    /// 股票板块
    /// </summary>
    public class StockSectorEnumModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// 板块名称
        /// </summary>
        [Display(Name = "板块名称")]
        public string SectorName { get; set; }
    }
}
