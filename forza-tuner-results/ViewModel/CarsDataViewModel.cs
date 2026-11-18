using ForzaTunerResults.DataModel.DTOs;
using ForzaTunerResults.DataModel.Entities;
using ForzaTunerResults.Persistence;
using ForzaTunerResults.ViewModel.Commands;
using ForzaTunerResults.ViewModel.Dialog;
using ForzaTunerResults.ViewModel.Utils;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ForzaTunerResults.ViewModel
{
    public partial class CarsDataViewModel : AbstractViewModel
    {
        private static readonly ILogger<CarsDataViewModel> logger = App.CreateLogger<CarsDataViewModel>();

        private readonly IDialogService _dialogService;
        private readonly DatabaseQueryFactory _databaseQueryFactory;
        private readonly DatabaseCommandFactory _databaseCommandFactory;

        private ObservableCollection<Car> _cars = [];
        private ObservableCollection<Tune> _tunes = [];
        private Car? _selectedCar;

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

        public ObservableCollection<Tune> Tunes
        {
            get => _tunes;
            set
            {
                if (_tunes != value)
                {
                    _tunes = value;
                    OnPropertyChanged(nameof(Tunes));
                }
            }
        }

        public Car? SelectedCar
        {
            get => _selectedCar;
            set
            {
                if (_selectedCar != value)
                {
                    _selectedCar = value;
                    OnPropertyChanged(nameof(SelectedCar));
                }
            }
        }

        public ICommand AddCarCommand { get; }
        public ICommand AddTuneCommand { get; }
        private readonly PropertyChangedEventHandler selectedCarListener;

        public CarsDataViewModel(IDialogService dialogService, DatabaseQueryFactory databaseQueryFactory, DatabaseCommandFactory databaseCommandFactory)
        {
            _dialogService = dialogService;
            _databaseQueryFactory = databaseQueryFactory;
            _databaseCommandFactory = databaseCommandFactory;

            AddCarCommand = new RelayCommand(async param => await AddCar(), (param) => true);
            AddTuneCommand = new RelayCommand(async param => await AddTune(param as Car), (param) => param is not null);

            selectedCarListener = async (sender, args) => await HandlePropertyChanged(args);
            PropertyChanged += selectedCarListener;
        }

        private async Task HandlePropertyChanged(PropertyChangedEventArgs args)
        {
            if (args == null)
            {
                return;
            }
            if (args.PropertyName == nameof(SelectedCar))
            {
                await LoadTunes();
            }
        }

        public void OnClose()
        {
            PropertyChanged -= selectedCarListener;
        }

        public async Task LoadCars()
        {
            var getAllCarsQuery = _databaseQueryFactory.GetAllCarsQuery();
            var cars = await getAllCarsQuery.Execute();
            Cars = new ObservableCollection<Car>(cars);

            logger.LogInformation("Loaded cars: {Cars}", Cars);
        }

        public async Task LoadTunes()
        {
            Tunes = new ObservableCollection<Tune>([]);
            if (SelectedCar is null)
            {
                logger.LogWarning("No car is selected");
                return;
            }
            var loadTunesQuery = _databaseQueryFactory.GetTunesByCarQuery(SelectedCar.Id);
            var tunes = await loadTunesQuery.Execute();
            Tunes = new ObservableCollection<Tune>(tunes);
            logger.LogInformation("Loaded tunes: {Tunes}", tunes);
        }

        private async Task AddCar()
        {
            var dialogResult = await _dialogService.ShowAddCarDialog();

            if (dialogResult == null)
            {
                return;
            }

            var year = dialogResult.Year;
            var manufacturer = dialogResult.Manufacturer;
            var name = dialogResult.Name;

            if (year == null || manufacturer == null || name == null)
            {
                logger.LogWarning("Cannot add Car. Year: {Year}, Manufacturer: {Manufacturer}, Name: {Name}", year, manufacturer, name);
                return;
            }

            logger.LogInformation("Adding Car with Year: {Year}, Manufacturer: {Manufacturer}, Name: {Name}", year, manufacturer, name);
            var car = new CarEntity(0, (int)year, manufacturer, name);
            var createCarCommand = _databaseCommandFactory.CreateCarCommand();
            await createCarCommand.Execute(car);
            await LoadCars();
            logger.LogInformation("Added car: {car}", car);
        }

        private async Task AddTune(Car? selectedCar)
        {
            if (selectedCar is null)
            {
                logger.LogWarning("Cannot add Tune. Selected car is null.");
                return;
            }

            var dialogResult = await _dialogService.ShowAddTuneDialog(selectedCar);
            if (dialogResult == null)
            {
                return;
            }

            var tune = ToModel(dialogResult);

            var createTuneCommand = _databaseCommandFactory.CreateTuneCommand();
            await createTuneCommand.Execute(tune);
            await LoadTunes();

            logger.LogInformation("Adding tune for car: {car}", selectedCar);
        }

        private static TuneEntity ToModel(AddTuneDialogViewModel dialogModel)
        {
            return new TuneEntity(
                Id: 0,
                CarId: dialogModel.Car?.Id ?? throw new InvalidOperationException("Car not set"),
                CarClassId: dialogModel.SelectedCarClass?.Id ?? throw new InvalidOperationException("Car class not set"),
                TiresId: dialogModel.SelectedTires?.Id ?? throw new InvalidOperationException("Tires not set"),
                SuspensionId: dialogModel.SelectedSuspension?.Id ?? throw new InvalidOperationException("Suspension not set"),
                DrivetrainId: dialogModel.SelectedDrivetrain?.Id ?? throw new InvalidOperationException("Drivetrain not set"),
                GearboxId: dialogModel.SelectedGearbox?.Id ?? throw new InvalidOperationException("Gearbox not set"),
                FrontAeroId: dialogModel.SelectedFrontAero?.Id ?? throw new InvalidOperationException("Front Aero not set"),
                RearAeroId: dialogModel.SelectedRearAero?.Id ?? throw new InvalidOperationException("Rear aero not set"),
                Engine: dialogModel.Engine ?? throw new InvalidOperationException("Engine not set"),
                Pi: dialogModel.Pi,
                Weight: dialogModel.Weight,
                Power: dialogModel.Power,
                Torque: dialogModel.Torque,
                LateralGrip: dialogModel.LateralGrip,
                IsDefault: dialogModel.IsDefault ?? false
            );
        }
    }
}
