using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zn.Core.Tools
{
    /// <summary>
    /// 杂合帮组类
    /// </summary>
    public class ToolHelper
    {
        /// <summary>
        /// 根据股票代码返回股票上市地址   0 上海， 1 深证
        /// </summary>
        /// <param name="stockId"></param>
        /// <returns></returns>
        public static string GetStockType(string stockId)
        {
            int tmp;
            if (string.IsNullOrEmpty(stockId))
                throw new ArgumentNullException("stockId");
            if (stockId.Length != 6)
                throw new ArgumentException("股票代码不是6位数字");

            if (!int.TryParse(stockId, out tmp))
                throw new ArgumentException("输入股票代码有误");

            if (stockId.StartsWith("002") || stockId.StartsWith("000"))  //可以确定的是深证上市的股票是  002 开头的
                return "1";

            return "0"; 
        }
    }
}
