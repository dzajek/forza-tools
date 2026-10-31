using ForzaTunerResults.ViewModel;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml.Controls;

namespace ForzaTunerResults.Views
{
    public sealed partial class CarsDataPage : Page
    {
        public CarsDataViewModel ViewModel { get; }

        public CarsDataPage()
        {
            ViewModel = App.Current.Services.GetRequiredService<CarsDataViewModel>();
            _ = ViewModel.LoadCars();

            InitializeComponent();
        }
    }
}
