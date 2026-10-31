using ForzaTunerResults.ViewModel;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.UI.Xaml.Controls;

namespace ForzaTunerResults.Views
{
    public sealed partial class TiresDataPage : Page
    {
        private static readonly ILogger<TiresDataPage> logger = App.CreateLogger<TiresDataPage>();

        public TiresDataViewModel ViewModel;
        public GlobalViewModel GlobalStore { get; }

        public TiresDataPage()
        {
            ViewModel = App.Current.Services.GetRequiredService<TiresDataViewModel>();
            _ = ViewModel.LoadTires();

            GlobalStore = App.Current.Services.GetRequiredService<GlobalViewModel>();

            InitializeComponent();
        }
    }
}
