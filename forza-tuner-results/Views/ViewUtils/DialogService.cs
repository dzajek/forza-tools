using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Threading.Tasks;

namespace ForzaTunerResults.Views.ViewUtils
{
    internal class DialogService : IDialogService
    {
        private XamlRoot? _xamlRoot = null;
        public XamlRoot XamlRoot
        {
            get
            {
                ArgumentNullException.ThrowIfNull(_xamlRoot, "XamlRoot");
                return _xamlRoot;
            }
            set
            {
                _xamlRoot = value;
            }
        }

        public async Task<bool> ShowContentDialog(ContentDialogOptions options)
        {
            var dialog = new ContentDialog()
            {
                XamlRoot = XamlRoot,
                Title = options.Title,
                PrimaryButtonText = options.PrimaryButtonText,
                CloseButtonText = options.CloseButtonText,
                Content = options.Content,
                DefaultButton = ContentDialogButton.Primary
            };

            var result = await dialog.ShowAsync();
            return result == ContentDialogResult.Primary;
        }
    }
}
