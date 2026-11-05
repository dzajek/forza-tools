using ForzaTunerResults.ViewModel.Dialog;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml.Controls;

namespace ForzaTunerResults.Views.Dialog
{
    public sealed partial class SingleInputDialog : Page
    {
        public SingleInputDialogViewModel ViewModel { get; }

        public SingleInputDialog()
        {
            ViewModel = App.Current.Services.GetRequiredService<SingleInputDialogViewModel>();

            InitializeComponent();
        }
    }
}
