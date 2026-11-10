using ForzaTunerResults.ViewModel.Dialog;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.UI.Xaml.Controls;

namespace ForzaTunerResults.Views.Dialog
{
    public sealed partial class AddTrackDialog : Page
    {
        private static readonly ILogger<AddTrackDialog> logger = App.CreateLogger<AddTrackDialog>();

        public AddTrackDialogViewModel ViewModel { get; }

        public AddTrackDialog()
        {
            ViewModel = App.Current.Services.GetRequiredService<AddTrackDialogViewModel>();

            InitializeComponent();
        }
    }
}
