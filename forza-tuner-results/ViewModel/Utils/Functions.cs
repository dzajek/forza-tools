using Microsoft.UI.Xaml;
using System.Collections;

namespace ForzaTunerResults.ViewModel.Utils
{
    public partial class Functions
    {
        public static Visibility GetListVisibility(IEnumerable list, bool invert = false)
        {
            bool hasItems = list != null && list.GetEnumerator().MoveNext();
            if (invert)
            {
                hasItems = !hasItems;
            }
            return hasItems ? Visibility.Visible : Visibility.Collapsed;
        }
    }
}
