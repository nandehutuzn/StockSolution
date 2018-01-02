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
using System.Windows.Shapes;
using Zn.Core.Tools;
using Zn.Core.StockModel;

namespace Zn.Core.Stock.MainHost
{
    /// <summary>
    /// Win_StockInfo.xaml 的交互逻辑
    /// </summary>
    public partial class Win_StockInfo : Window
    {
        private ILog _log = Logger.Current;
        private IDataBaseService _service = DataBaseService.Default;

        public Win_StockInfo()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadSource();
        }

        private void LoadSource()
        {
            var source = _service.SectorEnumModels;
            cmbSector.ItemsSource = source;
            listBoxSector.ItemsSource = source;
            stockDataGrid.ItemsSource = _service.StockInfoModels;

            cmbSector.DisplayMemberPath = "SectorName";
            cmbSector.SelectedValuePath = "Id";
            listBoxSector.DisplayMemberPath = "SectorName";
            listBoxSector.SelectedValuePath = "Id";
        }

        private async void btnAddSector_Click(object sender, RoutedEventArgs e)
        {
            string name = txbSectorName.Text.Trim();
            if (!string.IsNullOrEmpty(name))
            {
                StockSectorEnumModel model = new StockSectorEnumModel()
                {
                    SectorName = name,
                };
                var result = _service.InsertSectorEnumModel(model);
                await result;
                if (result.Result == 1)
                {
                    MessageBox.Show("添加成功", "提示", MessageBoxButton.OK);
                    LoadSource();
                }
            }
        }

        private async void btnAddStock_Click(object sender, RoutedEventArgs e)
        {
            string stockId = txbStockId.Text;
            string stockName = txbStockName.Text;
            string stockSectorName = cmbSector.SelectedValue.ToString();
            string address = txbAddress.Text;
            double peRatio;
            double.TryParse(txbPE.Text, out peRatio);
            string type = radioSH.IsChecked == true ? "0" : "1";
            if (!string.IsNullOrEmpty(stockId) && !string.IsNullOrEmpty(stockName)&&!string.IsNullOrEmpty(stockSectorName))
            {
                StockInfoModel model = new StockInfoModel()
                {
                    Id = stockId,
                    Name = stockName,
                    Sector = stockSectorName,
                    Address = address,
                    PERatio = peRatio,
                    Type = type,
                };
                var result = _service.InsertStockInfoModel(model);
                await result;
                if (result.Result == 1)
                {
                    MessageBox.Show("添加成功", "提示", MessageBoxButton.OK);
                    LoadSource();
                }
            }
        }

    
    }
}
