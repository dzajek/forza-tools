using ForzaTunerResults.ViewModel.Dialog;
using ForzaTunerResults.ViewModel.Utils;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml.Controls;

namespace ForzaTunerResults.Views.Dialog
{
    public sealed partial class AddCarClassDialog : Page, IDialogWithModel<AddCarClassDialogViewModel>
    {
        public AddCarClassDialogViewModel ViewModel { get; }

        public AddCarClassDialog()
        {
            ViewModel = App.Current.Services.GetRequiredService<AddCarClassDialogViewModel>();

            InitializeComponent();
        }
    }
}
