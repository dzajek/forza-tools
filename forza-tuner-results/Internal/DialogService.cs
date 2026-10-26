
using ForzaTunerResults.Views;
using Microsoft.UI.Xaml.Controls;

namespace ForzaTunerResults.Internal
{
    public sealed class DialogService(MainWindow mainWindow)
    {

        public ContentDialog BuildContentDialog(string Title, object content)
        {
            return new()
            {
                XamlRoot = mainWindow.Content.XamlRoot,
                Title = Title,
                Content = content
            };
        }

    }
}
