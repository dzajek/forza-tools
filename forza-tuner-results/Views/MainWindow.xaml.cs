using ForzaTunerResults.DataModel;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System.Collections.ObjectModel;


namespace ForzaTunerResults.Views
{

    public sealed partial class MainWindow : Window
    {
        public MainWindow(Collection<CarClass> carClasses)
        {
            this.CarClasses = new ObservableCollection<CarClass>(carClasses);
            InitializeComponent();
            // Extend the content into the title bar and hide the default titlebar
            this.ExtendsContentIntoTitleBar = true;
            
            // Set the custom title bar
            this.SetTitleBar(titleBar);
        }

        public ObservableCollection<CarClass> CarClasses { get; }

        private void TitleBar_PaneToggleRequested(TitleBar sender, object args)
        {
            mainNav.IsPaneOpen = !mainNav.IsPaneOpen;
        }
    }
}
