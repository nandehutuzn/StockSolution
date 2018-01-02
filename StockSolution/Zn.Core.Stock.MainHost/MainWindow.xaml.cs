using System;
using System.Collections.Generic;
using System.Linq;
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

namespace Zn.Core.Stock.MainHost
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private ILog _log = Logger.Current;
        private const string _key = "AB5B05D5A02E48838E5EDCD96D65132A";

        public MainWindow()
        {
            InitializeComponent();
        }

        //新浪地址  http://blog.csdn.net/littlesmallless/article/details/59171161

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            //string url = string.Format("http://stock.liangyee.com/bus-api/stock/freeStockMarketData/getDailyKBar?userKey={0}&startDate={1}&symbol={2}&endDate={3}&type={4}", _key, "2017-08-30", "603019", "2017-12-30", "0");
            //url = "http://hq.sinajs.cn/list=sh601006";//深证  sz=
            //var result = HttpHelper.GetHttpString(url);//第一个地址需用 UTF-8字符
            //await result;
            //MessageBox.Show(result.Result);

        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnEnd_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnSetting_Click(object sender, RoutedEventArgs e)
        {
            Win_StockInfo win = new Win_StockInfo();
            win.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterOwner;
            win.Owner = Application.Current.MainWindow;
            win.ShowDialog();
        }
    }
}
