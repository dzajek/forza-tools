using ForzaTunerResults.ViewModel;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml.Controls;

namespace ForzaTunerResults.Views
{
    public sealed partial class TiresDataPage : Page
    {
        public TiresDataViewModel ViewModel;
        public TiresDataPage()
        {
            InitializeComponent();
            ViewModel = App.Current.Services.GetRequiredService<TiresDataViewModel>();
            _ = ViewModel.LoadTires();
        }
    }
}
