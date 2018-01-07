using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;

namespace Zn.Core.Tools
{
    /// <summary>
    /// 消息发送中心
    /// </summary>
    public class MessageManager
    {
        #region Fieds

        private static ILog _log = Logger.Current;
        private static ConcurrentDictionary<string, List<Action<object>>> _dicAction = new ConcurrentDictionary<string, List<Action<object>>>();
        
        #endregion

        #region Func

        /// <summary>
        /// 注册消息
        /// </summary>
        /// <param name="messageKey"></param>
        /// <param name="callback"></param>
        public static void Register(string messageKey, Action<object> callback)
        {
            if (string.IsNullOrEmpty(messageKey))
                throw new ArgumentNullException("messageKey");
            if (callback == null)
                throw new ArgumentNullException("callback");
            try
            {
                if (_dicAction.ContainsKey(messageKey))
                {
                    _dicAction[messageKey].Add(callback);
                }
                else
                {
                    List<Action<object>> val = new List<Action<object>>() { callback };
                    _dicAction.TryAdd(messageKey, val);
                }
            }
            catch (Exception ex)
            {
                _log.Error(ex);
            }
        }

        /// <summary>
        /// 注销消息接收
        /// </summary>
        /// <param name="messageKey"></param>
        /// <param name="callback"></param>
        public static void UnRegister(string messageKey, Action<object> callback)
        {
            if (string.IsNullOrEmpty(messageKey))
                throw new ArgumentNullException("messageKey");
            if (callback == null)
                throw new ArgumentNullException("callback");

            if (_dicAction.ContainsKey(messageKey))
            {
                try
                {
                    _dicAction[messageKey].Remove(callback);
                }
                catch (Exception ex)
                {
                    _log.Error(ex);
                }
            }
        }

        /// <summary>
        /// 广播消息
        /// </summary>
        /// <param name="messageKey"></param>
        /// <param name="param"></param>
        public static Task NotifyMessage(string messageKey, object param)
        {
            if (string.IsNullOrEmpty(messageKey))
                throw new ArgumentNullException("messageKey");
            if (param == null)
                throw new ArgumentNullException("param");
            try
            {
                if (_dicAction.ContainsKey(messageKey))
                {
                    return Task.Run(() => _dicAction[messageKey].ForEach(o => o.Invoke(param)));
                }
            }
            catch (Exception ex)
            {
                _log.Error(ex);
            }
            return Task.Delay(2);
        }

        #endregion
    }
}
