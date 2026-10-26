using ForzaTunerResults.ViewModel;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml.Controls;

namespace ForzaTunerResults.Views
{
    public sealed partial class TrackTypesDataPage : Page
    {

        public TrackTypesDataViewModel ViewModel;

        public TrackTypesDataPage()
        {
            InitializeComponent();

            ViewModel = App.Current.Services.GetRequiredService<TrackTypesDataViewModel>();
            _ = ViewModel.LoadTrackTypes();
        }
    }
}
