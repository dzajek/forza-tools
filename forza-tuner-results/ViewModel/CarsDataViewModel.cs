
using ForzaTunerResults.DataModel;
using ForzaTunerResults.Service;
using ForzaTunerResults.ViewModel.Commands;
using ForzaTunerResults.Views.Dialog;
using Microsoft.Extensions.Logging;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ForzaTunerResults.ViewModel
{
    public partial class CarsDataViewModel : AbstractViewModel
    {

        private static readonly ILogger<CarsDataViewModel> logger = App.CreateLogger<CarsDataViewModel>();
        private readonly CarService carsService;
        private ObservableCollection<Car> _cars = [];

        public ICommand AddCarCommand { get; }
        public ObservableCollection<Car> Cars
        {
            get => _cars;
            set
            {
                if (_cars != value)
                {
                    _cars = value;
                    OnPropertyChanged(nameof(Cars));
                }
            }
        }

        public CarsDataViewModel(CarService carsService)
        {
            this.carsService = carsService;

            AddCarCommand = new DialogBasedCommand(async parent => await AddCar(parent));
        }

        public async Task LoadCars()
        {
            var cars = await carsService.LoadCarsAsync();
            Cars = new ObservableCollection<Car>(cars);

            logger.LogInformation("Loaded cars: {Cars}", Cars);
        }

        private async Task AddCar(UIElement parent)
        {
            var dialogContent = new AddCarDialog();
            ContentDialog dialog = new()
            {
                XamlRoot = parent.XamlRoot,
                Title = "Add Car",
                Content = dialogContent,
                PrimaryButtonText = "Add",
                CloseButtonText = "Cancel",
                DefaultButton = ContentDialogButton.Primary
            };

            var result = await dialog.ShowAsync();
            if (result != ContentDialogResult.Primary)
            {
                logger.LogInformation("Add Car dialog was canceled.");
                return;
            }

            var year = dialogContent.ViewModel.Year;
            var manufacturer = dialogContent.ViewModel.Manufacturer;
            var name = dialogContent.ViewModel.Name;

            if (year == null || manufacturer == null || name == null)
            {
                logger.LogWarning("Cannot add Car. Year: {Year}, Manufacturer: {Manufacturer}, Name: {Name}", year, manufacturer, name);
                return;
            }

            logger.LogInformation("Adding Car with Year: {Year}, Manufacturer: {Manufacturer}, Name: {Name}", year, manufacturer, name);
            var car = new Car(0, (int)year, manufacturer, name);
            await carsService.AddCarAsync(car);
            await LoadCars();
            logger.LogInformation("Added car: {car}", car);
        }

    }
}
