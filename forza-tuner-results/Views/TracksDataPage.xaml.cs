using ForzaTunerResults.ViewModel;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml.Controls;

namespace ForzaTunerResults.Views
{
    public sealed partial class TracksDataPage : Page
    {
        public TracksDataViewModel ViewModel { get; }

        public TracksDataPage()
        {
            InitializeComponent();

            ViewModel = App.Current.Services.GetRequiredService<TracksDataViewModel>();
            _ = ViewModel.LoadTracks();
        }
    }
}

