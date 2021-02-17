using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Data;
using ActionFlow.API;

namespace ActionFlow.HelloWorld.Converters
{
    [ValueConversion(typeof(eCustomerStatus), typeof(int))]
    class CustomerStatusToIntConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is eCustomerStatus e)
            {
                return (int)e;
            }

            return -1;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is int i)
            {
                return (eCustomerStatus) i;
            }

            return default(eCustomerStatus);
        }
    }
}
