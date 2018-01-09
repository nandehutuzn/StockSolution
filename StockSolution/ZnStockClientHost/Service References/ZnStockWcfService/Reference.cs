﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace ZnStockClientHost.ZnStockWcfService {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ZnStockWcfService.IStockWcfService", CallbackContract=typeof(ZnStockClientHost.ZnStockWcfService.IStockWcfServiceCallback))]
    public interface IStockWcfService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStockWcfService/Login", ReplyAction="http://tempuri.org/IStockWcfService/LoginResponse")]
        ZnStockClientHost.ZnStockWcfService.LoginResponse Login(ZnStockClientHost.ZnStockWcfService.LoginRequest request);
        
        // CODEGEN: 正在生成消息协定，应为该操作具有多个返回值。
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStockWcfService/Login", ReplyAction="http://tempuri.org/IStockWcfService/LoginResponse")]
        System.Threading.Tasks.Task<ZnStockClientHost.ZnStockWcfService.LoginResponse> LoginAsync(ZnStockClientHost.ZnStockWcfService.LoginRequest request);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IStockWcfServiceCallback {
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IStockWcfService/PushMessage")]
        void PushMessage(string message);
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.MessageContractAttribute(WrapperName="Login", WrapperNamespace="http://tempuri.org/", IsWrapped=true)]
    public partial class LoginRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=0)]
        public string name;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=1)]
        public string pwd;
        
        public LoginRequest() {
        }
        
        public LoginRequest(string name, string pwd) {
            this.name = name;
            this.pwd = pwd;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.MessageContractAttribute(WrapperName="LoginResponse", WrapperNamespace="http://tempuri.org/", IsWrapped=true)]
    public partial class LoginResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=0)]
        public int LoginResult;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=1)]
        public string returnMsg;
        
        public LoginResponse() {
        }
        
        public LoginResponse(int LoginResult, string returnMsg) {
            this.LoginResult = LoginResult;
            this.returnMsg = returnMsg;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IStockWcfServiceChannel : ZnStockClientHost.ZnStockWcfService.IStockWcfService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class StockWcfServiceClient : System.ServiceModel.DuplexClientBase<ZnStockClientHost.ZnStockWcfService.IStockWcfService>, ZnStockClientHost.ZnStockWcfService.IStockWcfService {
        
        public StockWcfServiceClient(System.ServiceModel.InstanceContext callbackInstance) : 
                base(callbackInstance) {
        }
        
        public StockWcfServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName) : 
                base(callbackInstance, endpointConfigurationName) {
        }
        
        public StockWcfServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, string remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public StockWcfServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public StockWcfServiceClient(System.ServiceModel.InstanceContext callbackInstance, System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, binding, remoteAddress) {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        ZnStockClientHost.ZnStockWcfService.LoginResponse ZnStockClientHost.ZnStockWcfService.IStockWcfService.Login(ZnStockClientHost.ZnStockWcfService.LoginRequest request) {
            return base.Channel.Login(request);
        }
        
        public int Login(string name, string pwd, out string returnMsg) {
            ZnStockClientHost.ZnStockWcfService.LoginRequest inValue = new ZnStockClientHost.ZnStockWcfService.LoginRequest();
            inValue.name = name;
            inValue.pwd = pwd;
            ZnStockClientHost.ZnStockWcfService.LoginResponse retVal = ((ZnStockClientHost.ZnStockWcfService.IStockWcfService)(this)).Login(inValue);
            returnMsg = retVal.returnMsg;
            return retVal.LoginResult;
        }
        
        public System.Threading.Tasks.Task<ZnStockClientHost.ZnStockWcfService.LoginResponse> LoginAsync(ZnStockClientHost.ZnStockWcfService.LoginRequest request) {
            return base.Channel.LoginAsync(request);
        }
    }
}