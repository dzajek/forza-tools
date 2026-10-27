using ForzaTunerResults.Service;
using ForzaTunerResults.ViewModel;
using ForzaTunerResults.ViewModel.Dialog;
using ForzaTunerResults.Views;
using Microsoft.Extensions.DependencyInjection;

namespace ForzaTunerResults.Internal
{
    internal class ServiceProviderConfigurer
    {
        public static ServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();

            // Services
            services.AddSingleton<DatabaseService>(provider =>
            {
                var dbService = new DatabaseService();
                dbService.InitializeDb();
                return dbService;
            });
            services.AddSingleton<DialogService>(provider =>
            {
                var mainWindow = provider.GetRequiredService<MainWindow>();
                return new DialogService(mainWindow);
            });
            services.AddSingleton<CarClassService>();
            services.AddSingleton<CarService>();
            services.AddSingleton<TiresService>();
            services.AddSingleton<TrackTypeService>();

            // Windows
            services.AddSingleton<MainWindow>();

            // ViewModels
            services.AddTransient<AddCarDialogViewModel>();
            services.AddTransient<AddTiresDialogViewModel>();
            services.AddTransient<AddTrackTypeDialogViewModel>();
            services.AddTransient<CarsDataViewModel>();
            services.AddTransient<MainWindowViewModel>();
            services.AddTransient<TiresDataViewModel>();
            services.AddTransient<TrackTypesDataViewModel>();

            return services.BuildServiceProvider();
        }

    }
}
