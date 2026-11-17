using ForzaTunerResults.ViewModel.Utils;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Threading.Tasks;

namespace ForzaTunerResults.Views.ViewUtils
{
    public interface IDialogService
    {
        Func<XamlRoot> XamlRootProvider { get; set; }

        public Task<bool> ShowContentDialog<T>(ContentDialogOptions<T> options) where T : Page;
    }
}
