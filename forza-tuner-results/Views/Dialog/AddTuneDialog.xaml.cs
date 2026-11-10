using ForzaTunerResults.ViewModel.Dialog;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml.Controls;

namespace ForzaTunerResults.Views.Dialog
{
    public sealed partial class AddTuneDialog : Page
    {
        public AddTuneDialogViewModel ViewModel { get; }

        public AddTuneDialog()
        {
            ViewModel = App.Current.Services.GetRequiredService<AddTuneDialogViewModel>();

            InitializeComponent();
        }
    }
}
