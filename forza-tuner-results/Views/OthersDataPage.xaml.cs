using ForzaTunerResults.ViewModel;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml.Controls;

namespace ForzaTunerResults.Views
{
    public sealed partial class OthersDataPage : Page
    {
        public OthersPageViewModel ViewModel { get; }

        public OthersDataPage()
        {
            ViewModel = App.Current.Services.GetRequiredService<OthersPageViewModel>();

            InitializeComponent();
        }
    }
}
