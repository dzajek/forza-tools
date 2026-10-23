using ForzaTunerResults.DataModel;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;

namespace ForzaTunerResults.Views
{
    public sealed partial class CarClassPage : Page
    {

        public string CarClassName { get; set; } = "unknown";
        private readonly ILogger<CarClassPage> _logger = App.CreateLogger<CarClassPage>();

        public CarClassPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            this._logger.LogInformation("Navigated to CarClassPage");

            if (e.Parameter is CarClass carClass)
            {
                CarClassName = carClass.Name;
            }
        }

    }
}
