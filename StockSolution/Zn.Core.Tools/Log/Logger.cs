using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;
using System.Reflection;

namespace Zn.Core.Tools
{
    /// <summary>
    /// 单例模式日志记录
    /// </summary>
    public class Logger : ILog
    {
        #region Fileds

        private static Logger _current;

        /// <summary>
        /// 监控日志是否正在写入 0 false，1 true
        /// </summary>
        private int _resourceInUse;

        #endregion

        #region Constructor

        private Logger() { }

        #endregion

        #region Properties

        /// <summary>
        /// 日志记录器
        /// </summary>
        public static Logger Current {
            get {
                if (_current == null)
                {
                    Interlocked.CompareExchange(ref _current, new Logger(), null);
                }
                return _current;
            }
        }

        #endregion

        #region Func

        private void WriteLog(string message, string filePath)
        {
            if (!string.IsNullOrEmpty(message))
            {
                try
                {
                    while (true)
                    {
                        if (Interlocked.Exchange(ref _resourceInUse, 1) == 0)//如果资源未被占用，则开始写入,并将值置为正占用
                        {
                            if (!Directory.Exists(Path.GetDirectoryName(filePath)))
                                Directory.CreateDirectory(Path.GetDirectoryName(filePath));
                            using (StreamWriter sWrite = new StreamWriter(filePath, true))
                            {
                                sWrite.WriteLine(message);
                            }
                            break;
                        }
                    }
                }
                finally
                {
                    Interlocked.CompareExchange(ref _resourceInUse, 0, 1);//将资源重置为未被占用
                }
            }
        }

        /// <summary>
        /// 获取日记地址
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private string GetLogFilePath(string type)
        {

            return Path.Combine(Environment.CurrentDirectory, "Log", type,
                DateTime.Now.ToString("yyyyMM"), string.Format("{0}.txt", DateTime.Now.Day));
        }

        private Task Log(string message, Exception exception, string type)
        {
            return Task.Run(() =>
            {
                string filePath = GetLogFilePath(type);
                string logMessage = null;
                if (!string.IsNullOrEmpty(message))
                    logMessage = string.Format("{0}:{1}Message: {2}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff"), type, message);
                if (exception != null)
                    logMessage += string.Format("{0}:{1}Exception: {2}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff"), type, exception.StackTrace);
                WriteLog(logMessage, filePath);
            });
        }

        #endregion

        #region ILog

        public Task Debug(string message)
        {
            return Log(message, null, MethodBase.GetCurrentMethod().Name);
        }

        public Task Debug(Exception exception)
        {
            return Log(null, exception, MethodBase.GetCurrentMethod().Name);
        }

        public Task Debug(string message, Exception exception)
        {
            return Log(message, exception, MethodBase.GetCurrentMethod().Name);
        }

        public Task Error(string message)
        {
            return Log(message, null, MethodBase.GetCurrentMethod().Name);
        }

        public Task Error(Exception exception)
        {
            return Log(null, exception, MethodBase.GetCurrentMethod().Name);
        }

        public Task Error(string message, Exception exception)
        {
            return Log(message, exception, MethodBase.GetCurrentMethod().Name);
        }

        public Task Fatal(string message)
        {
            return Log(message, null, MethodBase.GetCurrentMethod().Name);
        }

        public Task Fatal(Exception exception)
        {
            return Log(null, exception, MethodBase.GetCurrentMethod().Name);
        }

        public Task Fatal(string message, Exception exception)
        {
            return Log(message, exception, MethodBase.GetCurrentMethod().Name);
        }

        public Task Info(string message)
        {
            return Log(message, null, MethodBase.GetCurrentMethod().Name);
        }

        public Task Info(Exception exception)
        {
            return Log(null, exception, MethodBase.GetCurrentMethod().Name);
        }

        public Task Info(string message, Exception exception)
        {
            return Log(message, exception, MethodBase.GetCurrentMethod().Name);
        }

        public Task Warn(string message)
        {
            return Log(message, null, MethodBase.GetCurrentMethod().Name);
        }

        public Task Warn(Exception exception)
        {
            return Log(null, exception, MethodBase.GetCurrentMethod().Name);
        }

        public Task Warn(string message, Exception exception)
        {
            return Log(message, exception, MethodBase.GetCurrentMethod().Name);
        }

        #endregion
    }
}
