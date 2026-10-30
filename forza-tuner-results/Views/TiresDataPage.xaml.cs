using ForzaTunerResults.ViewModel;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.UI.Xaml.Controls;
using System.ComponentModel;

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

        public void OnLoaded(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {
            GlobalStore.PropertyChanged += OnViewModelPropertyChanged;
        }

        public void OnUnloaded(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {
            GlobalStore.PropertyChanged -= OnViewModelPropertyChanged;
        }

        private void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            logger.LogInformation("ViewModel property changed: {PropertyName}", e.PropertyName);
        }

    }
}
