using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Zn.Core.StockService.WCF
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的接口名“IStockWcfService”。
    [ServiceContract(CallbackContract = typeof(ICallback))]
    public interface IStockWcfService  //支持回调的双工通信模式
    {
        [OperationContract]
        int Login(string name, string pwd, out string returnMsg);
    }
}
