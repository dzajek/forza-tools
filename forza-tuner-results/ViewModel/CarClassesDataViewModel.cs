
using ForzaTunerResults.DataModel;
using ForzaTunerResults.Internal;
using ForzaTunerResults.Service;
using ForzaTunerResults.ViewModel.Commands;
using ForzaTunerResults.Views.Dialog;
using Microsoft.Extensions.Logging;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ForzaTunerResults.ViewModel
{
    public partial class CarClassesDataViewModel : AbstractViewModel
    {
        private static readonly ILogger<CarClassesDataViewModel> logger = App.CreateLogger<CarClassesDataViewModel>();

        private readonly CarClassService carClassService;
        private readonly DialogService dialogService;

        private ObservableCollection<CarClass> _carClasses = [];

        public ICommand AddCarClassCommand { get; }

        public ObservableCollection<CarClass> CarClasses
        {
            get => _carClasses;
            set
            {
                if (_carClasses != value)
                {
                    _carClasses = value;
                    OnPropertyChanged(nameof(CarClasses));
                }
            }
        }

        public CarClassesDataViewModel(CarClassService carClassService, DialogService dialogService)
        {
            this.carClassService = carClassService;
            this.dialogService = dialogService;
            AddCarClassCommand = new RelayCommand(async () => await AddCarClass(), () => true);
        }

        public async Task LoadCarClasses()
        {
            var carClasses = await carClassService.LoadCarClassesAsync();
            CarClasses = new ObservableCollection<CarClass>(carClasses);

            logger.LogInformation("Loaded car classes: {CarClasses}", CarClasses);
        }

        private async Task AddCarClass()
        {
            var dialogContent = new AddCarClassDialog();
            ContentDialog dialog = dialogService.BuildContentDialog("Add Car Class", dialogContent);
            dialog.PrimaryButtonText = "Add";
            dialog.CloseButtonText = "Cancel";
            dialog.DefaultButton = ContentDialogButton.Primary;

            var result = await dialog.ShowAsync();
            if (result != ContentDialogResult.Primary)
            {
                logger.LogInformation("Add Car Class dialog was canceled.");
                return;
            }

            var letter = dialogContent.ViewModel.Letter;
            var piLimit = dialogContent.ViewModel.PiLimit;

            if (letter == null || piLimit == null)
            {
                logger.LogWarning("Car class letter or PI limit is empty. Aborting add operation.");
                return;
            }

            logger.LogInformation("Adding Car Class with Letter: {Letter}, PI Limit: {PiLimit}", letter, piLimit);
            var newCarClass = new CarClass(0, letter, (int)piLimit);
            await carClassService.AddCarClassAsync(newCarClass);
            await LoadCarClasses();

            logger.LogInformation("Added new car class: {CarClass}", newCarClass);
        }
    }
}
