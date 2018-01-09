using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Threading.Tasks;

namespace Zn.Core.StockService.WCF
{
    /// <summary>
    /// WCF 回调接口
    /// </summary>
    public interface ICallback
    {
        [OperationContract(IsOneWay = true)]
        void PushMessage(string message);
    }
}
