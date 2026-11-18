using ForzaTunerResults.ViewModel.Dialog;
using ForzaTunerResults.ViewModel.Utils;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml.Controls;

namespace ForzaTunerResults.Views.Dialog
{
    public sealed partial class AddCarDialog : Page, IDialogWithModel<AddCarDialogViewModel>
    {
        public AddCarDialogViewModel ViewModel { get; }

        public AddCarDialog()
        {
            ViewModel = App.Current.Services.GetRequiredService<AddCarDialogViewModel>();

            InitializeComponent();
        }
    }
}
