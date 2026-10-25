using ForzaTunerResults.ViewModel;
using ForzaTunerResults.ViewModel.Dialog;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.UI.Xaml.Controls;


namespace ForzaTunerResults.Views.Dialog
{
    public sealed partial class AddCarDialog : Page
    {

        private readonly ILogger<AddCarDialog> logger = App.CreateLogger<AddCarDialog>();

        public AddCarDialogViewModel ViewModel { get; }

        public AddCarDialog()
        {
            InitializeComponent();
            ViewModel = App.Current.Services.GetRequiredService<AddCarDialogViewModel>();
        }
    }
}
