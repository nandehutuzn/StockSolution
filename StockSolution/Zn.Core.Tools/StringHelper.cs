using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zn.Core.Tools
{
    public static class StringHelper
    {
        public static string Reversed(this string source)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = source.Length - 1; i >= 0; i--)
            {
                sb.Append(source[i], 1);
            }

            return sb.ToString();
        }
    }
}
