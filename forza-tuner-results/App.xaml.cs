using ForzaTunerResults.DataModel;
using ForzaTunerResults.Service;
using ForzaTunerResults.Views;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.UI.Xaml;
using System;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;

namespace ForzaTunerResults
{

    public partial class App : Application
    {
        //private readonly IServiceProvider _serviceProvider;

        public new static App Current => (App)Application.Current;

        public IServiceProvider Services { get; }
        private Window? _window;

        public App()
        {
            Services = ConfigureServices();
            InitializeComponent();
        }

        protected override void OnLaunched(LaunchActivatedEventArgs args)
        {
            _window = Services.GetRequiredService<MainWindow>();
            _window.Activate();
        }

        private static IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();
            services.AddLogging(builder =>
            {
                builder.AddConsole();
                builder.AddDebug();
            });

            services.AddSingleton<CarClassService>();

            services.AddTransient<MainWindow>();

            return services.BuildServiceProvider();
        }

    }

}
