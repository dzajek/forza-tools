using ForzaTunerResults.ViewModel;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.UI.Xaml.Controls;

namespace ForzaTunerResults.Views
{
    public sealed partial class CarsDataPage : Page
    {
        private static readonly ILogger<CarsDataPage> logger = App.CreateLogger<CarsDataPage>();

        public CarsDataViewModel ViewModel { get; }

        public CarsDataPage()
        {
            ViewModel = App.Current.Services.GetRequiredService<CarsDataViewModel>();
            _ = ViewModel.LoadCars();

            InitializeComponent();
        }
    }
}
