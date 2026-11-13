using Microsoft.UI.Xaml.Data;
using System;

namespace ForzaTunerResults.ViewModel.Utils
{
    public partial class TimeSpanToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is TimeSpan timeSpan)
            {
                string format;
                if (timeSpan.Hours > 0)
                {
                    format = @"h\:mm\:ss\.fff";
                }
                else if (timeSpan.Minutes > 0)
                {
                    format = @"m\:ss\.fff";
                }
                else
                {
                    format = @"ss\.fff";
                }
                return timeSpan.ToString(format);
            }
            return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new InvalidOperationException("TimeSpanToStringConverter does not support ConvertBack.");
        }
    }
}
