using ForzaTunerResults.DataModel;
using ForzaTunerResults.DataModel.DTOs;
using ForzaTunerResults.Persistence;
using ForzaTunerResults.Views;
using Microsoft.UI.Dispatching;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace ForzaTunerResults.ViewModel
{
    public partial class MainWindowViewModel : AbstractViewModel
    {
        public GlobalViewModel GlobalStore { get; }
        private readonly DatabaseQueryFactory _databaseQueryFactory;

        private ObservableCollection<NavigationItem> _mainNavigationItems = [];
        public ObservableCollection<NavigationItem> MainNavigationItems
        {
            get => _mainNavigationItems;
            private set
            {
                if (_mainNavigationItems != value)
                {
                    _mainNavigationItems = value;
                    OnPropertyChanged(nameof(MainNavigationItems));
                }
            }
        }

        public ObservableCollection<NavigationItem> FooterMenuItems { get; } = new ObservableCollection<NavigationItem>([
            new("Data", typeof(DataPage), null, [])
        ]);

        private readonly PropertyChangedEventHandler carClassListChangeListener;

        public MainWindowViewModel(GlobalViewModel globalViewModel, DatabaseQueryFactory databaseQueryFactory)
        {
            _databaseQueryFactory = databaseQueryFactory;
            carClassListChangeListener = (sender, args) =>
            {
                _ = HandlePropertyChanged(args);
            };

            GlobalStore = globalViewModel;
            GlobalStore.PropertyChanged += carClassListChangeListener;
        }

        public async Task InitializeAsync()
        {
            if (GlobalStore.IsInitialized)
            {
                await RebuildNavigationItems();
            }
        }

        private async Task HandlePropertyChanged(PropertyChangedEventArgs args)
        {
            if (args == null)
            {
                return;
            }
            if (args.PropertyName == nameof(GlobalStore.CarClasses) || args.PropertyName == nameof(GlobalStore.TrackTypes))
            {
                await RebuildNavigationItems();
            }
        }

        private async Task RebuildNavigationItems()
        {
            DispatcherQueue currentQueue = DispatcherQueue.GetForCurrentThread();
            var getAllTracksQuery = _databaseQueryFactory.GetAllTracksQuery();
            var tracks = await getAllTracksQuery.Execute();
            var tracksMap = tracks
                .GroupBy(track => track.TypeId)
                .ToDictionary(
                    item => item.Key,
                    item => item.ToList()
                );

            var navigationItems = new List<NavigationItem>();
            foreach (var trackType in GlobalStore.TrackTypes)
            {
                var trackItems = new List<NavigationItem>();
                foreach (var track in tracksMap.GetValueOrDefault<long, List<Track>>(trackType.Id, []))
                {
                    var carClassItems = new List<NavigationItem>();
                    foreach (var carClass in GlobalStore.CarClasses)
                    {
                        carClassItems.Add(new NavigationItem(
                            carClass.Name,
                            typeof(ClassResultsPage),
                            new NavigationParamSet(trackType.Id, track.Id, carClass.Id),
                            []));
                    }

                    trackItems.Add(new NavigationItem(
                        track.Name,
                        typeof(ClassResultsPage),
                        null,
                        [.. carClassItems]));
                }

                navigationItems.Add(new NavigationItem(
                    trackType.Name,
                    typeof(ClassResultsPage),
                    null,
                    [.. trackItems]));
            }

            MainNavigationItems = new ObservableCollection<NavigationItem>(navigationItems);
        }

        public void OnClose()
        {
            PropertyChanged -= carClassListChangeListener;
        }

    }
}
