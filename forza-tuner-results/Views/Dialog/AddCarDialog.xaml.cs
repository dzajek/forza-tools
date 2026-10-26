using ForzaTunerResults.ViewModel.Dialog;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml.Controls;

namespace ForzaTunerResults.Views.Dialog
{
    public sealed partial class AddCarDialog : Page
    {
        public AddCarDialogViewModel ViewModel { get; }

        public AddCarDialog()
        {
            InitializeComponent();
            ViewModel = App.Current.Services.GetRequiredService<AddCarDialogViewModel>();
        }
    }
}
