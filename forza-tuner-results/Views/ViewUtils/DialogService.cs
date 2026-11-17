using ForzaTunerResults.ViewModel.Utils;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Threading.Tasks;

namespace ForzaTunerResults.Views.ViewUtils
{
    internal class DialogService : IDialogService
    {
        private Func<XamlRoot>? _xamlRootProvider = null;
        public Func<XamlRoot> XamlRootProvider
        {
            get
            {
                ArgumentNullException.ThrowIfNull(_xamlRootProvider, "XamlRootProvider");
                return _xamlRootProvider;
            }
            set
            {
                _xamlRootProvider = value;
            }
        }

        public async Task<bool> ShowContentDialog<T>(ContentDialogOptions<T> options) where T : Page
        {
            var dialog = new ContentDialog()
            {
                XamlRoot = XamlRootProvider(),
                Title = options.Title,
                PrimaryButtonText = options.PrimaryButtonText,
                CloseButtonText = options.CloseButtonText,
                Content = options.Content,
                DefaultButton = ContentDialogButton.Primary
            };
            dialog.Resources["ContentDialogMaxWidth"] = 1080;

            var result = await dialog.ShowAsync();
            return result == ContentDialogResult.Primary;
        }
    }
}
