using ForzaTunerResults.DataModel;
using ForzaTunerResults.Service;
using Microsoft.Extensions.Logging;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System.Collections.ObjectModel;


namespace ForzaTunerResults.Views
{

    public sealed partial class MainWindow : Window
    {

        public MainWindow(CarClassService carClassService)
        {
            InitializeComponent();
            
            this.ExtendsContentIntoTitleBar = true;
            this.SetTitleBar(titleBar);
            
            var carClasses = carClassService.GetCarClasses();
            this.CarMenuItems = new ObservableCollection<CarClass>(carClasses);
            this.FooterMenuItems = new ObservableCollection<string> {
                "Data"
            };
        }

        private readonly ILogger<MainWindow> logger = App.CreateLogger<MainWindow>();

        public ObservableCollection<CarClass> CarMenuItems{ get; }

        public ObservableCollection<string> FooterMenuItems { get; }

        private void TitleBar_PaneToggleRequested(TitleBar sender, object args)
        {
            mainNav.IsPaneOpen = !mainNav.IsPaneOpen;
        }

        private void NavigationView_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            if ((args.InvokedItemContainer is null) || (args.InvokedItemContainer is not NavigationViewItem))
            {
                return;
            }

            var source = (args.InvokedItemContainer as NavigationViewItem)?.Tag;

            if (source is CarClass)
            {
                logger.LogInformation("Navigating to CarClassPage");
                rootFrame.Navigate(typeof(CarClassPage), source);
            }
            else if (source is string tag && tag == "Data")
            {
                logger.LogInformation("Navigating to DataPage");
                rootFrame.Navigate(typeof(DataPage));
            }
            else
            {
                logger.LogWarning("No navigation defined for tag {tag}", source);
            }
        }

    }
}
