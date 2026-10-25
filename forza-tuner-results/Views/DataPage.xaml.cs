using Microsoft.Extensions.Logging;
using Microsoft.UI.Xaml.Controls;
using System;

namespace ForzaTunerResults.Views
{
    public sealed partial class DataPage : Page
    {
        private static readonly ILogger<DataPage> logger = App.CreateLogger<DataPage>();

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
                logger.LogWarning("No valid navigation found");
                return;
            }
            logger.LogInformation("Navigating to {source}", source);
            ContentFrame.Navigate(Type.GetType(source));
        }

    }

}
