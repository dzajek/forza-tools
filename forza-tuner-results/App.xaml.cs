using ForzaTunerResults.Internal;
using ForzaTunerResults.Service;
using ForzaTunerResults.ViewModel;
using ForzaTunerResults.Views;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.UI.Xaml;

namespace ForzaTunerResults
{

    public partial class App : Application
    {

        public new static App Current => (App)Application.Current;

        public ServiceProvider Services { get;  }
        public ILoggerFactory AppLoggerFactory { get; }

        public App()
        {
            AppLoggerFactory = LoggerFactory.Create(builder => builder.AddConsole().AddDebug());
            Services = ServiceProviderConfigurer.ConfigureServices(DatabaseConfigurer.InitializeDatabase());
            InitializeComponent();
        }

        protected override void OnLaunched(LaunchActivatedEventArgs args)
        {
           var _window = Services.GetRequiredService<MainWindow>();
            _window.Activate();
        }

        public static ILogger<T> CreateLogger<T>()
        {
            return Current.AppLoggerFactory.CreateLogger<T>();
        }

        public static ILogger CreateLogger(string name)
        {
            return Current.AppLoggerFactory.CreateLogger(name);
        }

    }

}
