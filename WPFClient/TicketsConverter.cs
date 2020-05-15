using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace WPFClient
{
    /**
     * A jegyek elérhetőségének kiírásánál bool értékek helyett rendes szöveget szeretnénk látni
     */
    class TicketsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is bool)) return "";
            return (bool)value ? "Tickets available!" : "Sold out!";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
