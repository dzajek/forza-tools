using Microsoft.UI.Xaml.Controls;

namespace ForzaTunerResults.ViewModel.Utils
{
    public record ContentDialogOptions<T>(
        string Title,
        string PrimaryButtonText,
        string CloseButtonText,
        T Content
    ) where T : Page;
}
