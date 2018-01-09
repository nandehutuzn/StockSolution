using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading;
using Zn.Core.Tools;

namespace Zn.Core.StockService.WCF
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的类名“StockWcfService”。
    public class StockWcfService : IStockWcfService
    {
        #region Fields

        private static List<ICallback> _lstCallback = new List<ICallback>();

        #endregion

        #region Constructor


        #endregion

        #region Properties

        public static List<ICallback> LstCallback { get { return _lstCallback; } }

        #endregion

        #region Interface

        public int Login(string name, string pwd, out string returnMsg)
        {
            ICallback callback = OperationContext.Current.GetCallbackChannel<ICallback>();
            _lstCallback.Add(callback);
            string sessionId = OperationContext.Current.SessionId;
            MessageManager.NotifyMessage(MessageKey.OPERATEMESSAGE, string.Format("用户{0}登陆成功!", name));
            OperationContext.Current.Channel.Closing += (s, e) =>
                {
                    lock (_lstCallback)
                    {
                        _lstCallback.Remove(callback);
                        MessageManager.NotifyMessage(MessageKey.OPERATEMESSAGE, string.Format("用户{0}已下线"));
                    }
                };
            returnMsg = "";
            return 0;
        }

        #endregion
    }
}
