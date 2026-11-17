using ForzaTunerResults.DataModel;
using ForzaTunerResults.ViewModel;
using ForzaTunerResults.ViewModel.Utils;
using ForzaTunerResults.Views.ViewUtils;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;


namespace ForzaTunerResults.Views
{
    public sealed partial class MainWindow : Window
    {
        private static readonly ILogger<MainWindow> logger = App.CreateLogger<MainWindow>();

        public MainWindowViewModel ViewModel { get; }

        public MainWindow()
        {
            ViewModel = App.Current.Services.GetRequiredService<MainWindowViewModel>();
            InitializeComponent();

            var globalStoreService = App.Current.Services.GetRequiredService<GlobalStoreService>();
            _ = globalStoreService.InitializeAsync();

            ExtendsContentIntoTitleBar = true;
            SetTitleBar(MainTitleBar);

            IDialogService dialogService = App.Current.Services.GetRequiredService<IDialogService>();
            dialogService.XamlRootProvider = () => MainGrid.XamlRoot;
        }

        private NavigationParamSet? _currentNavigationParam;

        private void MainPaneToggleRequested(TitleBar sender, object args)
        {
            MainNav.IsPaneOpen = !MainNav.IsPaneOpen;
        }

        internal void NavigationItemSelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            if ((args.SelectedItemContainer is null) || (args.SelectedItemContainer is not NavigationViewItem))
            {
                return;
            }
            var selectedItem = args.SelectedItem;

            if (selectedItem is not NavigationItem)
            {
                logger.LogWarning("No navigation defined for item {selectedItem}", selectedItem);
                return;
            }
            var navigationTarget = ((NavigationItem)selectedItem).NavigationTarget;
            var navigationParam = ((NavigationItem)selectedItem).NavigationParam;
            if (navigationTarget != typeof(DataPage) && navigationParam is null)
            {
                logger.LogWarning("No navigation parameter defined for item {selectedItem}", selectedItem);
                return;
            }

            if (RootFrame.Content != null && RootFrame.Content.GetType() == navigationTarget)
            {
                if (navigationParam == _currentNavigationParam)
                {
                    logger.LogInformation("Already on {targetType}, no navigation needed: {selectedItem}", navigationTarget, _currentNavigationParam);
                    return;
                }
            }

            logger.LogInformation("Navigating to {targetType}", navigationTarget);
            RootFrame.Navigate(navigationTarget, navigationParam);
            _currentNavigationParam = navigationParam;
        }

        private void MainWindow_Closed(object _sender, WindowEventArgs _args)
        {
            ViewModel.OnClose();
        }
    }
}
