using ForzaTunerResults.Internal;
using ForzaTunerResults.ViewModel;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;


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

            this.ExtendsContentIntoTitleBar = true;
            this.SetTitleBar(titleBar);
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
            Type? targetType = null;

            if (source is string tag && tag == "Data")
            {
                targetType = typeof(DataPage);
            }

            if (targetType is null)
            {
                logger.LogWarning("No navigation defined for tag {tag}", source);
                return;
            }

            if (rootFrame.Content != null && rootFrame.Content.GetType() == targetType)
            {
                logger.LogInformation("Already on {targetType}, no navigation needed", targetType);
                return;
            }

            logger.LogInformation("Navigating to {targetType}", targetType);
            rootFrame.Navigate(targetType);
        }

    }
}
