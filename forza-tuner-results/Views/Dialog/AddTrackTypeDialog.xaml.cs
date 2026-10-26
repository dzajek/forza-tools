using ForzaTunerResults.ViewModel.Dialog;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml.Controls;

namespace ForzaTunerResults.Views.Dialog
{
    public sealed partial class AddTrackTypeDialog : Page
    {
        public AddTrackTypeDialogViewModel ViewModel { get; }

        public AddTrackTypeDialog()
        {
            InitializeComponent();
            ViewModel = App.Current.Services.GetRequiredService<AddTrackTypeDialogViewModel>();
        }
    }
}
