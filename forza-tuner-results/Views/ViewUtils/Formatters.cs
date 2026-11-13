using System;

namespace ForzaTunerResults.Views.ViewUtils
{
    public sealed class Formatters
    {
        public static string FormatTimeSpan(TimeSpan? timeSpan)
        {
            if (!timeSpan.HasValue)
            {
                return "";
            }
            if (timeSpan.Value.Hours > 0)
            {
                return timeSpan.Value.ToString(@"h\:mm\:ss\.fff");
            }
            else if (timeSpan.Value.Minutes > 0)
            {
                return timeSpan.Value.ToString(@"m\:ss\.fff");
            }
            else
            {
                return timeSpan.Value.ToString(@"ss\.fff");
            }
        }

        public static string FormatDouble2(double? value)
        {
            if (!value.HasValue)
            {
                return "";
            }

            return value.Value.ToString("0.00");
        }

        public static string FormatDouble1(double? value)
        {
            if (!value.HasValue)
            {
                return "";
            }

            return value.Value.ToString("0.0");
        }

    }
}
