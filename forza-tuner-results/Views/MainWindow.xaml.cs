using ForzaTunerResults.DataModel;
using ForzaTunerResults.Service;
using Microsoft.Extensions.Logging;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System.Collections.ObjectModel;
using System.Linq;


namespace ForzaTunerResults.Views
{

    public sealed partial class MainWindow : Window
    {

        public MainWindow(CarClassService carClassService, ILogger<MainWindow> logger)
        {
            InitializeComponent();
            
            this.ExtendsContentIntoTitleBar = true;
            this.SetTitleBar(titleBar);
            this._logger = logger;
            
            var carClasses = carClassService.GetCarClasses();
            this.CarMenuItems = new ObservableCollection<MenuItem>(carClasses.Select(c => new MenuItem("CarClass" + c.Id, c.Name, "List")));
            this.FooterMenuItems = new ObservableCollection<MenuItem> {
                new("", "Data", "List")
            };

            _logger.LogInformation("Initialized MainWindow with {CarMenuItemsCount} car menu items and {FooterMenuItemsCount} footer menu items.", CarMenuItems.Count, FooterMenuItems.Count);
        }

        private readonly ILogger<MainWindow> _logger;

        public ObservableCollection<MenuItem> CarMenuItems{ get; }

        public ObservableCollection<MenuItem> FooterMenuItems { get; }

        private void TitleBar_PaneToggleRequested(TitleBar sender, object args)
        {
            mainNav.IsPaneOpen = !mainNav.IsPaneOpen;
        }
    }
}
