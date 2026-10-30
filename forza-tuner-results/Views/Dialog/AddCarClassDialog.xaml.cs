using ForzaTunerResults.ViewModel.Dialog;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml.Controls;


namespace ForzaTunerResults.Views.Dialog
{
    public sealed partial class AddCarClassDialog : Page
    {
        public AddCarClassDialogViewModel ViewModel { get; }

        public AddCarClassDialog()
        {
            ViewModel = App.Current.Services.GetRequiredService<AddCarClassDialogViewModel>();

            InitializeComponent();
        }
    }
}
