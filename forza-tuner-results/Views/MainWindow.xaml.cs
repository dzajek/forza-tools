using ForzaTunerResults.ViewModel;
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
            InitializeComponent();

            this.ExtendsContentIntoTitleBar = true;
            this.SetTitleBar(titleBar);

            ViewModel = App.Current.Services.GetRequiredService<MainWindowViewModel>();
            _ = ViewModel.LoadMenuItems();
        }

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

            if (source is string tag && tag == "Data")
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
