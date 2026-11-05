using ForzaTunerResults.Service;
using ForzaTunerResults.ViewModel.Commands;
using ForzaTunerResults.Views.Dialog;
using ForzaTunerResults.Views.ViewUtils;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ForzaTunerResults.ViewModel
{
    public sealed partial class DictionariesPageViewModel : AbstractViewModel
    {
        private static readonly ILogger<DictionariesPageViewModel> logger = App.CreateLogger<DictionariesPageViewModel>();

        private readonly IDialogService _dialogService;
        private readonly DictionaryItemService _dictionaryItemService;

        public GlobalViewModel GlobalStore;

        public ICommand AddTiresCommand { get; }
        public ICommand AddTrackTypeCommand { get; }
        public ICommand AddSuspensionCommand { get; }
        public ICommand AddDrivetrainCommand { get; }
        public ICommand AddGearboxCommand { get; }
        public ICommand AddAeroCommand { get; }

        public DictionariesPageViewModel(
            IDialogService dialogService,
            DictionaryItemService dictionaryItemService,
            GlobalViewModel globalViewModel)
        {
            _dialogService = dialogService;
            _dictionaryItemService = dictionaryItemService;
            GlobalStore = globalViewModel;

            AddTiresCommand = new RelayCommand(async param => await AddDictionaryItem(DictionaryItemType.Tires), () => true);
            AddTrackTypeCommand = new RelayCommand(async param => await AddDictionaryItem(DictionaryItemType.TrackType), () => true);
            AddSuspensionCommand = new RelayCommand(async param => await AddDictionaryItem(DictionaryItemType.Suspension), () => true);
            AddDrivetrainCommand = new RelayCommand(async param => await AddDictionaryItem(DictionaryItemType.Drivetrain), () => true);
            AddGearboxCommand = new RelayCommand(async param => await AddDictionaryItem(DictionaryItemType.Gearbox), () => true);
            AddAeroCommand = new RelayCommand(async param => await AddDictionaryItem(DictionaryItemType.Aero), () => true);
        }

        public async Task AddDictionaryItem(DictionaryItemType itemType)
        {
            var itemName = await GetValueFromDialog($"Add {itemType.Label}", itemType.Label);
            if (string.IsNullOrWhiteSpace(itemName))
            {
                logger.LogWarning("Cannot add {ItemType}. Name is null or whitespace.", itemType.Label);
                return;
            }
            await _dictionaryItemService.AddItemAsync(itemType, new(0, itemName));

            var items = await _dictionaryItemService.LoadItemsAsync(itemType);
            GlobalStore.RefreshDictionaryItems(itemType, items);

            logger.LogInformation("Added {ItemType}: {ItemName} to table {TableName}", itemType.Label, itemName, itemType.TableName);
        }

        private async Task<string?> GetValueFromDialog(string title, string label)
        {
            var dialogContent = new SingleInputDialog();
            dialogContent.ViewModel.Label = label;
            var options = new ContentDialogOptions(
                Title: title,
                PrimaryButtonText: "OK",
                CloseButtonText: "Cancel",
                Content: dialogContent
            );
            var dialogApproved = await _dialogService.ShowContentDialog(options);
            if (!dialogApproved)
            {
                logger.LogInformation("Add Tires dialog was canceled.");
                return null;
            }
            return dialogContent.ViewModel.InputValue;
        }
    }
}
