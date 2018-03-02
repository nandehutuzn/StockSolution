using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Zn.Core.Tools
{
    /// <summary>
    /// by zhangning 2018/3/2
    /// </summary>
    public static class TaskFactoryExtension
    {
        /// <summary>
        /// 显式设置使用线程池调度器执行任务
        /// </summary>
        /// <param name="factory"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static Task StartNewInThreadPool(this TaskFactory factory, Action action)
        {
            return Task.Factory.StartNew(action, CancellationToken.None, TaskCreationOptions.None, TaskScheduler.Default);
        }

        public static Task StartNewInThreadPool(this TaskFactory factory, Action<object> action, object state)
        {
            return Task.Factory.StartNew(action, state, CancellationToken.None, TaskCreationOptions.None, TaskScheduler.Default);
        }

        public static Task StartNewInThreadPool(this TaskFactory factory, Action action, CancellationToken cancellationToken)
        {
            return Task.Factory.StartNew(action, cancellationToken, TaskCreationOptions.None, TaskScheduler.Default);
        }

        public static Task StartNewInThreadPool(this TaskFactory factory, Action action, TaskCreationOptions creationOptions)
        {
            return Task.Factory.StartNew(action, CancellationToken.None, creationOptions, TaskScheduler.Default);
        }

        public static Task StartNewInThreadPool(this TaskFactory factory, Action<object> action, object state, TaskCreationOptions creationOptions)
        {
            return Task.Factory.StartNew(action, state, CancellationToken.None, creationOptions, TaskScheduler.Default);
        }

        public static Task StartNewInThreadPool(this TaskFactory factory, Action<object> action, object state, CancellationToken cancellationToken)
        {
            return Task.Factory.StartNew(action, state, cancellationToken, TaskCreationOptions.None, TaskScheduler.Default);
        }

        public static Task<TResult> StartNewInThreadPool<TResult>(this TaskFactory factory, Func<TResult> function)
        {
            return Task.Factory.StartNew(function, CancellationToken.None, TaskCreationOptions.None, TaskScheduler.Default);
        }

        public static Task<TResult> StartNewInThreadPool<TResult>(this TaskFactory factory, Func<object, TResult> function, object state)
        {
            return Task.Factory.StartNew(function, state, CancellationToken.None, TaskCreationOptions.None, TaskScheduler.Default);
        }

        public static Task<TResult> StartNewInThreadPool<TResult>(this TaskFactory factory, Func<TResult> function, TaskCreationOptions creationOptions)
        {
            return Task.Factory.StartNew(function, CancellationToken.None, creationOptions, TaskScheduler.Default);
        }

        public static Task<TResult> StartNewInThreadPool<TResult>(this TaskFactory factory, Func<TResult> function, CancellationToken cancellationToken)
        {
            return Task.Factory.StartNew(function, cancellationToken, TaskCreationOptions.None, TaskScheduler.Default);
        }

        public static Task<TResult> StartNewInThreadPool<TResult>(this TaskFactory factory, Func<object, TResult> function, object state, TaskCreationOptions creationOptions)
        {
            return Task.Factory.StartNew(function, state, CancellationToken.None, creationOptions, TaskScheduler.Default);
        }

        public static Task<TResult> StartNewInThreadPool<TResult>(this TaskFactory factory, Func<object, TResult> function, object state, CancellationToken cancellationToken)
        {
            return Task.Factory.StartNew(function, state, cancellationToken, TaskCreationOptions.None, TaskScheduler.Default);
        }
    }
}
