using ForzaTunerResults.DataModel;
using ForzaTunerResults.Service;
using ForzaTunerResults.Views;
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
        private readonly TrackService _tracksService;

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

        public MainWindowViewModel(GlobalViewModel globalViewModel, TrackService tracksService)
        {
            GlobalStore = globalViewModel;
            _tracksService = tracksService;

            carClassListChangeListener = async (sender, args) => await HandlePropertyChanged(args);
            GlobalStore.PropertyChanged += carClassListChangeListener;
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
            var tracks = await _tracksService.LoadAllTracks();
            var tracksMap = tracks
                .GroupBy(track => track.Raw.TypeId)
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
                            new NavigationParamSet(trackType.Id, track.Raw.Id, carClass.Id),
                            []));
                    }

                    trackItems.Add(new NavigationItem(
                        track.Raw.Name,
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
