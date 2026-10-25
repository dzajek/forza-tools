
using ForzaTunerResults.Service;
using ForzaTunerResults.ViewModel;
using ForzaTunerResults.ViewModel.Dialog;
using ForzaTunerResults.Views;
using Microsoft.Extensions.DependencyInjection;

namespace ForzaTunerResults
{
    internal class ServiceProviderConfigurer
    {
        public static ServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();

            // Singleton services
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
