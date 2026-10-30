using ForzaTunerResults.ViewModel;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml.Controls;

namespace ForzaTunerResults.Views
{
    public sealed partial class CarClassesDataPage : Page
    {
        public CarClassesDataViewModel ViewModel { get; }

        public CarClassesDataPage()
        {
            ViewModel = App.Current.Services.GetRequiredService<CarClassesDataViewModel>();

            InitializeComponent();
        }
    }
}
