using ForzaTunerResults.ViewModel;
using Microsoft.Extensions.Logging;
using Microsoft.UI.Xaml.Controls;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using ForzaTunerResults.Views.Dialog;
using System;

namespace ForzaTunerResults.Views
{
    public sealed partial class CarsDataPage : Page
    {
        private readonly ILogger<CarsDataPage> logger = App.CreateLogger<CarsDataPage>();

        public CarsDataViewModel ViewModel { get; }

        public CarsDataPage()
        {
            InitializeComponent();

            ViewModel = App.Current.Services.GetRequiredService<CarsDataViewModel>();
            _ = ViewModel.LoadCars();
        }

        private async void AddCarButton_Click(object sender, RoutedEventArgs e)
        {
            var dialogContent = new AddCarDialog();
            ContentDialog dialog = new()
            {
                XamlRoot = this.XamlRoot,
                Title = "Add Car",
                Content = dialogContent,
                PrimaryButtonText = "Add",
                CloseButtonText = "Cancel",
                DefaultButton = ContentDialogButton.Primary
            };
            var result = await dialog.ShowAsync();
            if (result == ContentDialogResult.Primary)
            {
                logger.LogInformation("Adding new Car");
                var year = dialogContent.ViewModel.Year;
                var manufacturer = dialogContent.ViewModel.Manufacturer;
                var name = dialogContent.ViewModel.Name;

                if (year != null && manufacturer != null && name != null)
                {
                    logger.LogInformation("Adding Car with Year: {Year}, Manufacturer: {Manufacturer}, Name: {Name}", year, manufacturer, name);
                    await ViewModel.AddCar((int)year, manufacturer, name);
                }
                else
                {
                    logger.LogWarning("Cannot add Car. Year: {Year}, Manufacturer: {Manufacturer}, Name: {Name}", year, manufacturer, name);
                }

            }
        }
    }
}
