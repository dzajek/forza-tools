using ForzaTunerResults.DataModel;
using ForzaTunerResults.Service;
using ForzaTunerResults.Views;
using Microsoft.UI.Xaml;
using System.Collections.ObjectModel;

namespace ForzaTunerResults
{

    public partial class ForzaTunerResultsApplication : Application
    {
        private Window? _window;


        public ForzaTunerResultsApplication()
        {
            InitializeComponent();
        }

        protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
            Collection<CarClass> carClasses = CarClassService.GetCarClasses();

            _window = new MainWindow(carClasses);
            _window.Activate();
        }

    }

}
