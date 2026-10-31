
using ForzaTunerResults.DataModel;
using ForzaTunerResults.Service;
using ForzaTunerResults.ViewModel.Commands;
using ForzaTunerResults.Views.Dialog;
using ForzaTunerResults.Views.ViewUtils;
using Microsoft.Extensions.Logging;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ForzaTunerResults.ViewModel
{
    public partial class CarsDataViewModel : AbstractViewModel
    {
        private static readonly ILogger<CarsDataViewModel> logger = App.CreateLogger<CarsDataViewModel>();

        private readonly IDialogService _dialogService;
        private readonly CarService _carsService;
        private ObservableCollection<Car> _cars = [];

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

        public ICommand AddCarCommand { get; }

        public CarsDataViewModel(IDialogService dialogService, CarService carsService)
        {
            this._dialogService = dialogService;
            this._carsService = carsService;

            AddCarCommand = new RelayCommand(async param => await AddCar(), () => true);
        }

        public async Task LoadCars()
        {
            var cars = await _carsService.LoadCarsAsync();
            Cars = new ObservableCollection<Car>(cars);

            logger.LogInformation("Loaded cars: {Cars}", Cars);
        }

        private async Task AddCar()
        {
            var dialogContent = new AddCarDialog();

            var options = new ContentDialogOptions(
                Title: "Add Car",
                PrimaryButtonText: "Add",
                CloseButtonText: "Cancel",
                Content: dialogContent
            );

            bool dialogApproved = await _dialogService.ShowContentDialog(options);

            if (!dialogApproved)
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
            await _carsService.AddCarAsync(car);
            await LoadCars();
            logger.LogInformation("Added car: {car}", car);
        }
    }
}
