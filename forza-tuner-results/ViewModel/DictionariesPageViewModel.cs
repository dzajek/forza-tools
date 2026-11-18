using ForzaTunerResults.DataModel;
using ForzaTunerResults.Persistence;
using ForzaTunerResults.ViewModel.Commands;
using ForzaTunerResults.ViewModel.Utils;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ForzaTunerResults.ViewModel
{
    public sealed partial class DictionariesPageViewModel : AbstractViewModel
    {
        private static readonly ILogger<DictionariesPageViewModel> logger = App.CreateLogger<DictionariesPageViewModel>();

        private readonly IDialogService _dialogService;
        private readonly DatabaseCommandFactory _databaseCommandFactory;
        private readonly DatabaseQueryFactory _databaseQueryFactory;

        public GlobalViewModel GlobalStore;

        public ICommand AddTiresCommand { get; }
        public ICommand AddTrackTypeCommand { get; }
        public ICommand AddSuspensionCommand { get; }
        public ICommand AddDrivetrainCommand { get; }
        public ICommand AddGearboxCommand { get; }
        public ICommand AddAeroCommand { get; }

        public DictionariesPageViewModel(
            IDialogService dialogService,
            DatabaseQueryFactory databaseQueryFactory,
            DatabaseCommandFactory databaseCommandFactory,
            GlobalViewModel globalViewModel)
        {
            _dialogService = dialogService;
            _databaseQueryFactory = databaseQueryFactory;
            _databaseCommandFactory = databaseCommandFactory;

            GlobalStore = globalViewModel;
            AddTiresCommand = new RelayCommand(async param => await AddDictionaryItem(DictionaryItemType.Tires));
            AddTrackTypeCommand = new RelayCommand(async param => await AddDictionaryItem(DictionaryItemType.TrackType));
            AddSuspensionCommand = new RelayCommand(async param => await AddDictionaryItem(DictionaryItemType.Suspension));
            AddDrivetrainCommand = new RelayCommand(async param => await AddDictionaryItem(DictionaryItemType.Drivetrain));
            AddGearboxCommand = new RelayCommand(async param => await AddDictionaryItem(DictionaryItemType.Gearbox));
            AddAeroCommand = new RelayCommand(async param => await AddDictionaryItem(DictionaryItemType.Aero));
        }

        public async Task AddDictionaryItem(DictionaryItemType itemType)
        {
            var dialogResult = await _dialogService.ShowSingleInputDialog($"Add {itemType.Label}", itemType.Label);
            if (dialogResult == null)
            {
                return;
            }

            var itemName = dialogResult.InputValue;
            if (string.IsNullOrWhiteSpace(itemName))
            {
                logger.LogWarning("Cannot add {ItemType}. Name is null or whitespace.", itemType.Label);
                return;
            }
            var command = _databaseCommandFactory.CreateDictionaryItemCommand(itemType);
            await command.Execute(new(0, itemName));

            var query = _databaseQueryFactory.GetDictionaryItemsByTypeQuery(itemType);
            var items = await query.Execute();

            GlobalStore.RefreshDictionaryItems(itemType, items);

            logger.LogInformation("Added {ItemType}: {ItemName} to table {TableName}", itemType.Label, itemName, itemType.TableName);
        }

    }
}
