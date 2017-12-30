using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zn.Core.Tools
{
    /// <summary>
    /// 异步日志记录接口
    /// </summary>
    public interface ILog
    {
        Task Debug(string message);
        Task Debug(Exception exception);
        Task Debug(string message, Exception exception);

        Task Info(string message);
        Task Info(Exception exception);
        Task Info(string message, Exception exception);

        Task Warn(string message);
        Task Warn(Exception exception);
        Task Warn(string message, Exception exception);

        Task Error(string message);
        Task Error(Exception exception);
        Task Error(string message, Exception exception);

        Task Fatal(string message);
        Task Fatal(Exception exception);
        Task Fatal(string message, Exception exception);
    }
}
