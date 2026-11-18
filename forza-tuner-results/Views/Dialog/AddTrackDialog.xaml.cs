using ForzaTunerResults.ViewModel.Dialog;
using ForzaTunerResults.ViewModel.Utils;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml.Controls;

namespace ForzaTunerResults.Views.Dialog
{
    public sealed partial class AddTrackDialog : Page, IDialogWithModel<AddTrackDialogViewModel>
    {
        public AddTrackDialogViewModel ViewModel { get; }

        public AddTrackDialog()
        {
            ViewModel = App.Current.Services.GetRequiredService<AddTrackDialogViewModel>();

            InitializeComponent();
        }
    }
}
