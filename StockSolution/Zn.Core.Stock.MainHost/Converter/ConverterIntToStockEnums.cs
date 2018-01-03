using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using Zn.Core.StockModel;

namespace Zn.Core.Stock.MainHost.Converter
{
    class ConverterIntToStockEnums : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string val = value as string;
            if (val != null)
            { 
                int i;
                int.TryParse(val, out i);
                var result = DataBaseService.Default.SectorEnumModels().FirstOrDefault(o=>o.Id == i);
                if(result!=null)
                    return result.SectorName;
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
