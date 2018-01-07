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
        private TaskScheduler _uiScheduler;

        public Win_StockInfo()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _uiScheduler = TaskScheduler.FromCurrentSynchronizationContext();
            LoadSource();
        }

        private async void LoadSource()
        {
            Tuple<List<StockSectorEnumModel>, List<StockInfoModel>> result;
            result = await Task.Run(() =>
                {
                    return new Tuple<List<StockSectorEnumModel>, List<StockInfoModel>>(_service.SectorEnumModels(true), 
                        _service.StockInfoModels(true));
                });
            try
            {
                cmbSector.ItemsSource = result.Item1;
                listBoxSector.ItemsSource = result.Item1;
                stockDataGrid.ItemsSource = result.Item2;

                cmbSector.DisplayMemberPath = "SectorName";
                cmbSector.SelectedValuePath = "Id";
                listBoxSector.DisplayMemberPath = "SectorName";
                listBoxSector.SelectedValuePath = "Id";
            }
            catch (Exception ex)
            {
                _log.Error(ex);
            }
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
                var result = _service.Insert<StockSectorEnumModel>(model);
                await result;
                if (result.Result == 1)
                {
                    //MessageBox.Show("添加成功", "提示", MessageBoxButton.OK);
                    LoadSource();
                }
            }
        }

        private void btnAddStock_Click(object sender, RoutedEventArgs e)
        {
            string stockId = txbStockId.Text.Trim(); ;
            string stockName = txbStockName.Text.Trim() ;
            string stockSectorName = cmbSector.SelectedValue.ToString();
            string address = txbAddress.Text;
            double peRatio;
            double.TryParse(txbPE.Text, out peRatio);
            string type = ToolHelper.GetStockType(stockId);
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
                Task.Run(() =>
                {
                    var entities = _service.StockInfoModels(true).Where(o => o.Id == stockId);
                    if (entities != null && entities.Count() > 0) //该股票已经存在
                    {
                        MessageManager.NotifyMessage(MessageKey.OPERATEMESSAGE, string.Format("股票{0}:{1} 已经存在", stockId, stockName));
                        return -1;
                    }
                    return 0;
                }).ContinueWith(async t =>
                    {
                        if (t.Result == 0)
                        {
                            var result = await _service.Insert<StockInfoModel>(model);
                            if (result == 1)
                            {
                                //MessageBox.Show("添加成功", "提示", MessageBoxButton.OK);
                                LoadSource();
                            }
                        }
                    }, _uiScheduler);
            }
        }

    
    }
}
