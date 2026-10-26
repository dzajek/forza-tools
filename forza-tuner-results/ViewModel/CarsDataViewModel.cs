
using ForzaTunerResults.DataModel;
using ForzaTunerResults.Internal;
using ForzaTunerResults.Service;
using ForzaTunerResults.ViewModel.Commands;
using ForzaTunerResults.Views.Dialog;
using Microsoft.Extensions.Logging;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ForzaTunerResults.ViewModel
{
    public partial class CarsDataViewModel : INotifyPropertyChanged
    {

        private static readonly ILogger<CarsDataViewModel> logger = App.CreateLogger<CarsDataViewModel>();
        private readonly CarService carsService;
        private readonly DialogService dialogService;

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
        public event PropertyChangedEventHandler? PropertyChanged;


        public CarsDataViewModel(CarService carsService, DialogService dialogService)
        {
            this.carsService = carsService;
            this.dialogService = dialogService;

            AddCarCommand = new RelayCommand(async () => await AddCar(), () => true);
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public async Task LoadCars()
        {
            var cars = await carsService.LoadCarsAsync();
            Cars = new ObservableCollection<Car>(cars);

            logger.LogInformation("Loaded cars: {Cars}", Cars);
        }

        private async Task AddCar()
        {
            var dialogContent = new AddCarDialog();
            ContentDialog dialog = dialogService.BuildContentDialog("Add Car", dialogContent);
            dialog.PrimaryButtonText = "Add";
            dialog.CloseButtonText = "Cancel";
            dialog.DefaultButton = ContentDialogButton.Primary;

            var result = await dialog.ShowAsync();
            if (result != ContentDialogResult.Primary)
            {
                logger.LogInformation("Add Car dialog was canceled.");
                return;
            }

            logger.LogInformation("Adding new Car");
            var year = dialogContent.ViewModel.Year;
            var manufacturer = dialogContent.ViewModel.Manufacturer;
            var name = dialogContent.ViewModel.Name;

            if (year != null && manufacturer != null && name != null)
            {
                logger.LogInformation("Adding Car with Year: {Year}, Manufacturer: {Manufacturer}, Name: {Name}", year, manufacturer, name);
                var car = new Car(0, (int)year, manufacturer, name);
                await carsService.AddCarAsync(car);
                await LoadCars();
                logger.LogInformation("Added car: {car}", car);
            }
            else
            {
                logger.LogWarning("Cannot add Car. Year: {Year}, Manufacturer: {Manufacturer}, Name: {Name}", year, manufacturer, name);
            }
        }

    }
}
