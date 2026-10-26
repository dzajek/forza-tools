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

            // Services
            services.AddSingleton(typeof(DatabaseService), new DatabaseService(dbConnectionString));
            services.AddSingleton<DialogService>(provider =>
            {
                var mainWindow = provider.GetRequiredService<MainWindow>();
                return new DialogService(mainWindow);
            });
            services.AddSingleton<CarClassService>();
            services.AddSingleton<CarsService>();
            services.AddSingleton<TrackTypesService>();

            // Windows
            services.AddSingleton<MainWindow>();

            // ViewModels
            services.AddTransient<AddCarDialogViewModel>();
            services.AddTransient<AddTrackTypeDialogViewModel>();
            services.AddTransient<CarsDataViewModel>();
            services.AddTransient<MainWindowViewModel>();
            services.AddTransient<TrackTypesDataViewModel>();

            return services.BuildServiceProvider();
        }

    }
}
