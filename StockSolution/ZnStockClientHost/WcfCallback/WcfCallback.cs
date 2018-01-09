using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zn.Core.Tools;

namespace ZnStockClientHost
{
    public class WcfCallback : ZnStockWcfService.IStockWcfServiceCallback
    {
        public void PushMessage(string message)
        {
            MessageManager.NotifyMessage(MessageKey.OPERATEMESSAGE, string.Format("服务端发来消息: {0}", message));
        }
    }
}
