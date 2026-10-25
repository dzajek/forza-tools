
using ForzaTunerResults.DataModel;
using ForzaTunerResults.Service;
using Microsoft.Extensions.Logging;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;

namespace ForzaTunerResults.ViewModel
{
    public partial class CarsDataViewModel(CarsService carsService) : INotifyPropertyChanged
    {

        private readonly ILogger<CarsDataViewModel> logger = App.CreateLogger<CarsDataViewModel>();
        private readonly CarsService carsService = carsService;

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

        public event PropertyChangedEventHandler? PropertyChanged;

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

        internal async Task AddCar(int year, string manufacturer, string name)
        {
            var car = new Car(0, year, manufacturer, name);
            await carsService.AddCarAsync(car);
            await LoadCars();
            logger.LogInformation("Added car: {car}", car);
        }
    }
}
