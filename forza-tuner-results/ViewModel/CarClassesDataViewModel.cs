using ForzaTunerResults.DataModel.DTOs;
using ForzaTunerResults.DataModel.Entities;
using ForzaTunerResults.Persistence;
using ForzaTunerResults.ViewModel.Commands;
using ForzaTunerResults.Views.Dialog;
using ForzaTunerResults.Views.ViewUtils;
using Microsoft.Extensions.Logging;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ForzaTunerResults.ViewModel
{
    public partial class CarClassesDataViewModel : AbstractViewModel
    {
        private static readonly ILogger<CarClassesDataViewModel> logger = App.CreateLogger<CarClassesDataViewModel>();

        private readonly IDialogService _dialogService;
        private readonly DatabaseQueryFactory _databaseQueryFactory;
        private readonly DatabaseCommandFactory _databaseCommandFactory;

        public GlobalViewModel GlobalStore { get; }
        public ICommand AddCarClassCommand { get; }

        public CarClassesDataViewModel(
            IDialogService dialogService,
            DatabaseQueryFactory databaseQueryFactory,
            DatabaseCommandFactory databaseCommandFactory,
            GlobalViewModel globalViewModel)
        {
            _dialogService = dialogService;
            _databaseQueryFactory = databaseQueryFactory;
            _databaseCommandFactory = databaseCommandFactory;
            GlobalStore = globalViewModel;

            AddCarClassCommand = new RelayCommand(async param => await AddCarClass());
        }

        private async Task AddCarClass()
        {
            var dialogContent = new AddCarClassDialog();

            var options = new ContentDialogOptions(
                Title: "Add Car Class",
                PrimaryButtonText: "Add",
                CloseButtonText: "Cancel",
                Content: dialogContent
            );

            bool dialogApproved = await _dialogService.ShowContentDialog(options);

            if (!dialogApproved)
            {
                logger.LogInformation("Add Car Class dialog was canceled.");
                return;
            }

            var prefix = dialogContent.ViewModel.Prefix;
            var piLimit = dialogContent.ViewModel.PiLimit;

            if (prefix == null || piLimit == null)
            {
                logger.LogWarning("Car class Prefix or PI limit is empty. Aborting add operation.");
                return;
            }

            logger.LogInformation("Adding Car Class with Prefix: {Prefix}, PI Limit: {PiLimit}", prefix, piLimit);

            var newCarClass = new CarClassEntity(0, prefix, (int)piLimit);
            var addCarClassCommand = _databaseCommandFactory.CreateCarClassCommand();
            await addCarClassCommand.Execute(newCarClass);

            var getCarClassesQuery = _databaseQueryFactory.GetAllCarClassesQuery();
            var carClasses = await getCarClassesQuery.Execute();
            GlobalStore.CarClasses = new ObservableCollection<CarClass>(carClasses);

            logger.LogInformation("Added new car class: {CarClass}", newCarClass);
        }
    }
}
