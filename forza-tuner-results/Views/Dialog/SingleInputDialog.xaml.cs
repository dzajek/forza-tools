using ForzaTunerResults.ViewModel.Dialog;
using ForzaTunerResults.ViewModel.Utils;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml.Controls;

namespace ForzaTunerResults.Views.Dialog
{
    public sealed partial class SingleInputDialog : Page, IDialogWithModel<SingleInputDialogViewModel>
    {
        public SingleInputDialogViewModel ViewModel { get; }

        public SingleInputDialog()
        {
            ViewModel = App.Current.Services.GetRequiredService<SingleInputDialogViewModel>();

            InitializeComponent();
        }
    }
}
