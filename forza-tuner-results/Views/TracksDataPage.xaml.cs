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
            ViewModel = App.Current.Services.GetRequiredService<TracksDataViewModel>();
            InitializeComponent();
            _ = ViewModel.LoadTracks();

        }
    }
}

