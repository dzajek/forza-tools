using Microsoft.UI.Xaml.Data;
using System;


namespace ForzaTunerResults.ViewModel.Utils
{
    public partial class DoubleToIntConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is int i)
            {
                return System.Convert.ToDouble(i);
            }
            return 0.0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if (value is double d)
            {
                return System.Convert.ToInt32(d);
            }
            return 0;
        }
    }
}
