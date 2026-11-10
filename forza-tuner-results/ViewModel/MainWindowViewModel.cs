using ForzaTunerResults.DataModel;
using ForzaTunerResults.Views;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace ForzaTunerResults.ViewModel
{
    public partial class MainWindowViewModel : AbstractViewModel
    {
        public GlobalViewModel GlobalStore { get; }

        private ObservableCollection<NavigationItem> _mainNavigationItems = new ObservableCollection<NavigationItem>([]);
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
            new("Data", typeof(DataPage), null)
        ]);

        private readonly PropertyChangedEventHandler carClassListChangeListener;

        public MainWindowViewModel(GlobalViewModel globalViewModel)
        {
            GlobalStore = globalViewModel;

            carClassListChangeListener = async (sender, args) => await HandlePropertyChanged(args);
            GlobalStore.PropertyChanged += carClassListChangeListener;
        }

        private async Task HandlePropertyChanged(PropertyChangedEventArgs args)
        {
            if (args == null)
            {
                return;
            }
            if (args.PropertyName == nameof(GlobalStore.CarClasses))
            {
                MainNavigationItems = new ObservableCollection<NavigationItem>(
                    GlobalStore.CarClasses.Select(carClass => new NavigationItem(carClass.Name, typeof(ClassResultsPage), carClass.Id.ToString()))
                );
            }
        }

        public void OnClose()
        {
            PropertyChanged -= carClassListChangeListener;
        }

    }
}
