using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using KoncertManager.BLL.DTOs;

namespace WPFClient
{
    /**
     * A listában levő együttesek neveiből stringet készít, amit ki lehet írni a táblázatban
     */
    class BandsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string bands = string.Empty;
            foreach (Band band in value as List<Band>)
            {
                bands += band.Name;
                bands += ", ";
            }

            return bands.TrimEnd(',', ' ');  //Minden együttes után került vessző, utolsó után ne legyen
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
