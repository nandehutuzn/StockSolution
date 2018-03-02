using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Zn.Core.Tools
{
    /// <summary>
    /// http 帮助类
    /// </summary>
    public class HttpHelper
    {
        private static ILog _log = Logger.Current;

        /// <summary>
        /// 根据http地址，返回请求得到的数据
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static Task<string> GetHttpString(string url)
        {
            return GetHttpString(url, Encoding.Default);
        }
         
        /// <summary>
        /// 根据http地址，返回请求得到的数据
        /// </summary>
        /// <param name="url"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static Task<string> GetHttpString(string url, Encoding encoding)
        {
            if (string.IsNullOrEmpty(url))
                throw new ArgumentNullException("url");

            return Task.Run<string>(() =>
            {
                try
                {
                    HttpWebRequest request = WebRequest.CreateHttp(url);
                    request.Method = "GET";
                    using (Stream stream = request.GetResponse().GetResponseStream())
                    {
                        return new StreamReader(stream, encoding).ReadToEnd();
                    }
                }
                catch (Exception ex)
                {
                    _log.Error(string.Format("url: {0}", url), ex);
                    return null;
                }
            });
        }
    }
}
