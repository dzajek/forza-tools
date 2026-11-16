using ForzaTunerResults.DataModel;
using ForzaTunerResults.Persistence;
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
        DatabaseQueryFactory databaseQueryFactory
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

            var getTiresQuery = databaseQueryFactory.GetAllDictionaryItemsQuery(DictionaryItemType.Tires);
            var tires = await getTiresQuery.Execute();
            store.Tires = new ObservableCollection<DictionaryItem>(tires);

            var getTrackTypesQuery = databaseQueryFactory.GetAllDictionaryItemsQuery(DictionaryItemType.TrackType);
            var trackTypes = await getTrackTypesQuery.Execute();
            store.TrackTypes = new ObservableCollection<DictionaryItem>(trackTypes);

            var getSuspensionsQuery = databaseQueryFactory.GetAllDictionaryItemsQuery(DictionaryItemType.Suspension);
            var suspensions = await getSuspensionsQuery.Execute();
            store.Suspensions = new ObservableCollection<DictionaryItem>(suspensions);

            var getDrivetrainsQuery = databaseQueryFactory.GetAllDictionaryItemsQuery(DictionaryItemType.Drivetrain);
            var drivetrains = await getDrivetrainsQuery.Execute();
            store.Drivetrains = new ObservableCollection<DictionaryItem>(drivetrains);

            var getGearboxesQuery = databaseQueryFactory.GetAllDictionaryItemsQuery(DictionaryItemType.Gearbox);
            var gearboxes = await getGearboxesQuery.Execute();
            store.Gearboxes = new ObservableCollection<DictionaryItem>(gearboxes);

            var getAerosQuery = databaseQueryFactory.GetAllDictionaryItemsQuery(DictionaryItemType.Aero);
            var aeros = await getAerosQuery.Execute();
            store.Aeros = new ObservableCollection<DictionaryItem>(aeros);

            logger.LogInformation("Initialization done");
            store.IsInitialized = true;
        }

    }
}