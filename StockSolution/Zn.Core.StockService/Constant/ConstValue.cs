using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zn.Core.StockService.Constant
{
    /// <summary>
    /// 常量值
    /// </summary>
    class ConstValue
    {
        private const string KEYLIANG = "AB5B05D5A02E48838E5EDCD96D65132A";

        /// <summary>
        /// liangyee 免费http请求地址  {0}开始日期（yyyy-MM-dd）,{1} 股票代码 , {2} 结束日期, {3} 上证 0，深证 1
        /// </summary>
        public static string LIANGYEEURL = @"http://stock.liangyee.com/bus-api/stock/freeStockMarketData/getDailyKBar?userKey=AB5B05D5A02E48838E5EDCD96D65132A&startDate={0}&symbol={1}&endDate={2}&type={3}";

        //新浪地址  http://blog.csdn.net/littlesmallless/article/details/59171161

        /// <summary>
        /// 新浪免费实时股票信息地址 {0} 上证 sh，深证 sz，{1} 股票代码
        /// </summary>
        public const string XINAREALTIMEURL = "http://hq.sinajs.cn/list={0}{1}";
    }
}
