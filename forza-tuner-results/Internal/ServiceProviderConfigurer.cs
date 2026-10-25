using ForzaTunerResults.Service;
using ForzaTunerResults.ViewModel;
using ForzaTunerResults.ViewModel.Dialog;
using ForzaTunerResults.Views;
using Microsoft.Extensions.DependencyInjection;

namespace ForzaTunerResults.Internal
{
    internal class ServiceProviderConfigurer
    {
        public static ServiceProvider ConfigureServices(string dbConnectionString)
        {
            var services = new ServiceCollection();

            // Singleton services
            services.AddSingleton(typeof(DatabaseService), new DatabaseService(dbConnectionString));
            services.AddSingleton<CarClassService>();
            services.AddSingleton<CarsService>();

            // Windows
            services.AddTransient<MainWindow>();

            // ViewModels
            services.AddTransient<AddCarDialogViewModel>();
            services.AddTransient<CarsDataViewModel>();
            services.AddTransient<MainWindowViewModel>();

            return services.BuildServiceProvider();
        }

    }
}
