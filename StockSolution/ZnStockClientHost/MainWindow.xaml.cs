using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Zn.Core.Tools;
using ZnStockClientHost.ZnStockWcfService;
using System.Threading;

namespace ZnStockClientHost
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private ILog _log = Logger.Current;
        private SynchronizationContext _uiSyncCxt;

        public MainWindow()
        {
            InitializeComponent();
            _uiSyncCxt = SynchronizationContext.Current;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            MessageManager.Register(MessageKey.OPERATEMESSAGE, OperateCallback);
        }

        private void OperateCallback(object message)
        {
            string msg = message as string;
            if (msg != null)
                _uiSyncCxt.Post(o => startBtn.Content = message.ToString(), null);
        }

        private void startBtn_Click(object sender, RoutedEventArgs e)
        {
            IStockWcfServiceCallback callback = new WcfCallback();
            InstanceContext context = new InstanceContext(callback);
            StockWcfServiceClient client = new StockWcfServiceClient(context);
            string outMsg;
            client.Login("张宁", "123456", out outMsg);
        }
    }
}
