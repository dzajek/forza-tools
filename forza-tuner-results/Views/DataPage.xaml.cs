using ForzaTunerResults.ViewModel;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.UI.Xaml.Controls;
using System;
using System.ComponentModel;

namespace ForzaTunerResults.Views
{
    public sealed partial class DataPage : Page
    {
        private static readonly ILogger<DataPage> logger = App.CreateLogger<DataPage>();

        public GlobalViewModel GlobalStore { get; }

        public DataPage()
        {
            GlobalStore = App.Current.Services.GetRequiredService<GlobalViewModel>();

            InitializeComponent();
        }

        public void DataSelectorBar_SelectionChanged(SelectorBar sender, SelectorBarSelectionChangedEventArgs args)
        {
            SelectorBarItem selectedItem = sender.SelectedItem;
            var source = selectedItem.Tag.ToString();
            if (source is null)
            {
                logger.LogWarning("No valid navigation found");
                return;
            }
            logger.LogInformation("Navigating to {source}", source);
            ContentFrame.Navigate(Type.GetType(source));
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
