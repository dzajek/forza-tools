using ForzaTunerResults.DataModel;
using ForzaTunerResults.Service;
using ForzaTunerResults.ViewModel;
using Microsoft.Extensions.Logging;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace ForzaTunerResults.Internal
{
    public sealed class GlobalStoreService(
        GlobalViewModel store,
        CarClassService carClassService,
        DictionaryItemService dictionaryItemService
    )
    {
        private static readonly ILogger<GlobalStoreService> logger = App.CreateLogger<GlobalStoreService>();

        private readonly object _lock = new();
        private Task? _initializationTask;

        public Task InitializeAsync()
        {
            lock (_lock)
            {
                return _initializationTask ??= InitializeCoreAsync();
            }
        }

        private async Task InitializeCoreAsync()
        {
            if (store.IsInitialized)
                return;
            logger.LogInformation("Initializing...");
            var carClasses = await carClassService.LoadCarClassesAsync();
            store.CarClasses = new ObservableCollection<CarClass>(carClasses);

            var tires = await dictionaryItemService.LoadItemsAsync(DictionaryItemType.Tires);
            store.Tires = new ObservableCollection<DictionaryItem>(tires);

            var trackTypes = await dictionaryItemService.LoadItemsAsync(DictionaryItemType.TrackType);
            store.TrackTypes = new ObservableCollection<DictionaryItem>(trackTypes);

            var suspensions = await dictionaryItemService.LoadItemsAsync(DictionaryItemType.Suspension);
            store.Suspensions = new ObservableCollection<DictionaryItem>(suspensions);

            var drivetrains = await dictionaryItemService.LoadItemsAsync(DictionaryItemType.Drivetrain);
            store.Drivetrains = new ObservableCollection<DictionaryItem>(drivetrains);

            var gearboxes = await dictionaryItemService.LoadItemsAsync(DictionaryItemType.Gearbox);
            store.Gearboxes = new ObservableCollection<DictionaryItem>(gearboxes);

            var aeros = await dictionaryItemService.LoadItemsAsync(DictionaryItemType.Aero);
            store.Aeros = new ObservableCollection<DictionaryItem>(aeros);

            logger.LogInformation("Initialization done");
            store.IsInitialized = true;
        }

    }
}