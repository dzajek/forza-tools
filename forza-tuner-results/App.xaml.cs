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
            Services = ConfigureServices();
            AppLoggerFactory = LoggerFactory.Create(builder => builder.AddConsole().AddDebug());
            InitializeComponent();
        }

        protected override void OnLaunched(LaunchActivatedEventArgs args)
        {
           var _window = Services.GetRequiredService<MainWindow>();
            _window.Activate();
        }

        private static ServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();

            services.AddSingleton<CarClassService>();

            services.AddTransient<MainWindow>();
            services.AddTransient<MainWindowViewModel>();

            return services.BuildServiceProvider();
        }

        public static ILogger<T> CreateLogger<T>()
        {
            return Current.AppLoggerFactory.CreateLogger<T>();
        }

    }

}
