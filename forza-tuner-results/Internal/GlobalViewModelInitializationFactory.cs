using ForzaTunerResults.DataModel;
using ForzaTunerResults.DataModel.DTOs;
using ForzaTunerResults.Persistence;
using ForzaTunerResults.ViewModel;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace ForzaTunerResults.Internal
{
    internal class GlobalViewModelInitializationFactory
    {
        private static readonly ILogger<GlobalViewModelInitializationFactory> logger = App.CreateLogger<GlobalViewModelInitializationFactory>();

        internal static GlobalViewModel InitializeGlobalViewModel(IServiceProvider provider)
        {
            var databaseQueryFactory = provider.GetRequiredService<DatabaseQueryFactory>();
            var globalViewModel = new GlobalViewModel();

            Task.Run(() => InitializeCoreAsync(globalViewModel, databaseQueryFactory)).Wait();

            return globalViewModel;
        }

        private static async Task InitializeCoreAsync(GlobalViewModel store, DatabaseQueryFactory databaseQueryFactory)
        {
            if (store.IsInitialized)
                return;

            logger.LogInformation("Initializing GlobalViewModel...");

            var getCarClassesQuery = databaseQueryFactory.GetAllCarClassesQuery();
            var carClasses = await getCarClassesQuery.Execute();
            store.CarClasses = new ObservableCollection<CarClass>(carClasses);

            var getTiresQuery = databaseQueryFactory.GetDictionaryItemsByTypeQuery(DictionaryItemType.Tires);
            var tires = await getTiresQuery.Execute();
            store.Tires = new ObservableCollection<DictionaryItem>(tires);

            var getTrackTypesQuery = databaseQueryFactory.GetDictionaryItemsByTypeQuery(DictionaryItemType.TrackType);
            var trackTypes = await getTrackTypesQuery.Execute();
            store.TrackTypes = new ObservableCollection<DictionaryItem>(trackTypes);

            var getSuspensionsQuery = databaseQueryFactory.GetDictionaryItemsByTypeQuery(DictionaryItemType.Suspension);
            var suspensions = await getSuspensionsQuery.Execute();
            store.Suspensions = new ObservableCollection<DictionaryItem>(suspensions);

            var getDrivetrainsQuery = databaseQueryFactory.GetDictionaryItemsByTypeQuery(DictionaryItemType.Drivetrain);
            var drivetrains = await getDrivetrainsQuery.Execute();
            store.Drivetrains = new ObservableCollection<DictionaryItem>(drivetrains);

            var getGearboxesQuery = databaseQueryFactory.GetDictionaryItemsByTypeQuery(DictionaryItemType.Gearbox);
            var gearboxes = await getGearboxesQuery.Execute();
            store.Gearboxes = new ObservableCollection<DictionaryItem>(gearboxes);

            var getAerosQuery = databaseQueryFactory.GetDictionaryItemsByTypeQuery(DictionaryItemType.Aero);
            var aeros = await getAerosQuery.Execute();
            store.Aeros = new ObservableCollection<DictionaryItem>(aeros);

            store.IsInitialized = true;
            logger.LogInformation("GlobalViewModel initialized successfully.");
        }
    }
}
