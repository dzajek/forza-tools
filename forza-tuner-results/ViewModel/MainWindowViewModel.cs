using ForzaTunerResults.DataModel;
using ForzaTunerResults.Service;
using Microsoft.Extensions.Logging;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace ForzaTunerResults.ViewModel
{
    public partial class MainWindowViewModel(CarClassService carClassService) : AbstractViewModel
    {
        private static readonly ILogger<MainWindowViewModel> logger = App.CreateLogger<MainWindowViewModel>();
        private readonly CarClassService carClassService = carClassService;
        private ObservableCollection<CarClass> _carMenuItems = [];
        private ObservableCollection<string> _footerMenuItems = [];

        public ObservableCollection<CarClass> CarMenuItems
        {
            get => _carMenuItems;
            set
            {
                if (_carMenuItems != value)
                {
                    _carMenuItems = value;
                    OnPropertyChanged(nameof(CarMenuItems));
                }
            }
        }

        public ObservableCollection<string> FooterMenuItems
        {
            get => _footerMenuItems;
            set
            {
                if (_footerMenuItems != value)
                {
                    _footerMenuItems = value;
                    OnPropertyChanged(nameof(FooterMenuItems));
                }
            }
        }

        public async Task LoadMenuItems()
        {
            var carClasses = await carClassService.LoadCarClassesAsync();
            CarMenuItems = new ObservableCollection<CarClass>(carClasses);
            FooterMenuItems = new ObservableCollection<string>(["Data"]);

            logger.LogInformation("Loaded menu items: {CarMenuItems} and footer items: {FooterMenuItems}", CarMenuItems, FooterMenuItems);
        }

    }
}
