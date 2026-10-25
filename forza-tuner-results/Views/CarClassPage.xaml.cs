using ForzaTunerResults.DataModel;
using Microsoft.Extensions.Logging;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;

namespace ForzaTunerResults.Views
{
    public sealed partial class CarClassPage : Page
    {
        private static readonly ILogger<CarClassPage> logger = App.CreateLogger<CarClassPage>();

        public string CarClassName { get; set; } = "unknown";

        public CarClassPage()
        {
            InitializeComponent();
            logger.LogInformation("Initialized CarClass page");
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            logger.LogInformation("Navigated to CarClassPage");

            if (e.Parameter is CarClass carClass)
            {
                CarClassName = carClass.Name;
            }
        }

    }
}
