using Microsoft.Extensions.Logging;
using Microsoft.UI.Xaml.Controls;
using ForzaTunerResults.Views;
using System;

namespace ForzaTunerResults.Views
{
    public sealed partial class DataPage : Page
    {
        private readonly ILogger<DataPage> _logger = App.CreateLogger<DataPage>();

        public DataPage()
        {
            InitializeComponent();
        }

        public void DataSelectorBar_SelectionChanged(SelectorBar sender, SelectorBarSelectionChangedEventArgs args)
        {
            SelectorBarItem selectedItem = sender.SelectedItem;
            var source = selectedItem.Tag.ToString();
            if (source is null)
            {
                _logger.LogWarning("No valid navigation found");
                return;
            }
            _logger.LogInformation("Navigating to {source}", source);
            ContentFrame.Navigate(Type.GetType(source));
        }

    }

}
