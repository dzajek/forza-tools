
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
    public partial class CarClassesDataViewModel : AbstractViewModel
    {
        private static readonly ILogger<CarClassesDataViewModel> logger = App.CreateLogger<CarClassesDataViewModel>();

        private readonly CarClassService carClassService;

        public GlobalViewModel GlobalStore { get; }
        public ICommand AddCarClassCommand { get; }

        public CarClassesDataViewModel(GlobalViewModel globalViewModel, CarClassService carClassService)
        {
            this.GlobalStore = globalViewModel;
            this.carClassService = carClassService;
            AddCarClassCommand = new DialogBasedCommand(async parent => await AddCarClass(parent));
        }

        private async Task AddCarClass(UIElement parent)
        {
            var dialogContent = new AddCarClassDialog();
            ContentDialog dialog = new()
            {
                XamlRoot = parent.XamlRoot,
                Title = "Add Car Class",
                Content = dialogContent,
                PrimaryButtonText = "Add",
                CloseButtonText = "Cancel",
                DefaultButton = ContentDialogButton.Primary
            };

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
            var carClasses = await carClassService.LoadCarClassesAsync();
            GlobalStore.CarClasses = new ObservableCollection<CarClass>(carClasses);

            logger.LogInformation("Added new car class: {CarClass}", newCarClass);
        }
    }
}
