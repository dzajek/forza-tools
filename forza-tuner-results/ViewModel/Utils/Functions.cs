using Microsoft.UI.Xaml;
using System.Collections;

namespace ForzaTunerResults.ViewModel.Utils
{
    public partial class Functions
    {
        public static Visibility GetListVisibility(IEnumerable list, bool invert = false) => GetListVisibility(new(), list, invert);

        public static Visibility GetListVisibility(object? nonNullGuard, IEnumerable list, bool invert = false)
        {
            bool hasItems = list != null && list.GetEnumerator().MoveNext();
            if (invert)
            {
                hasItems = !hasItems;
            }
            return (hasItems && (nonNullGuard is not null)) ? Visibility.Visible : Visibility.Collapsed;
        }

        public static string? DisplayDouble(double? value)
        {
            return value?.ToString("0.##");
        }

    }
}
