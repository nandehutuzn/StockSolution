using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zn.Core.Tools
{
    public static class DataTableHelper
    {
        /// <summary>
        /// DataTable转string[] 队列(包括列名)
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static List<string[]> ToListArray(this DataTable dt)
        {
            List<string[]> resultList = new List<string[]>();
            string[] columns = new string[dt.Columns.Count];
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                columns[i] = dt.Columns[i].ColumnName;
            }
            resultList.Add(columns);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string[] cols = new string[dt.Columns.Count];
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    cols[j] = dt.Rows[i][j].ToString();
                }
                resultList.Add(cols);
            }

            return resultList;
        }
    }
}
