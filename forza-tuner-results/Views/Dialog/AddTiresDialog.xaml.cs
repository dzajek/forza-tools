using ForzaTunerResults.ViewModel.Dialog;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml.Controls;

namespace ForzaTunerResults.Views.Dialog
{
    public sealed partial class AddTiresDialog : Page
    {
        public AddTiresDialogViewModel ViewModel { get; }

        public AddTiresDialog()
        {
            ViewModel = App.Current.Services.GetRequiredService<AddTiresDialogViewModel>();

            InitializeComponent();
        }
    }
}
