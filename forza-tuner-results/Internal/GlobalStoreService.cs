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
        TrackTypeService trackTypeService
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
            var trackTypes = await trackTypeService.LoadTrackTypesAsync();
            store.TrackTypes = new ObservableCollection<TrackType>(trackTypes);

            var carClasses = await carClassService.LoadCarClassesAsync();
            store.CarClasses = new ObservableCollection<CarClass>(carClasses);

            logger.LogInformation("Initialization done");
            store.IsInitialized = true;
        }

    }
}