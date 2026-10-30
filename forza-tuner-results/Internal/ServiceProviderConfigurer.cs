using ForzaTunerResults.Service;
using ForzaTunerResults.ViewModel;
using ForzaTunerResults.ViewModel.Dialog;
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

            services.AddSingleton<CarClassService>();
            services.AddSingleton<CarService>();
            services.AddSingleton<TiresService>();
            services.AddSingleton<TrackService>();
            services.AddSingleton<TrackTypeService>();

            services.AddSingleton<GlobalStoreService>();

            // ViewModels
            services.AddTransient<AddCarDialogViewModel>();
            services.AddTransient<AddCarClassDialogViewModel>();
            services.AddTransient<AddTiresDialogViewModel>();
            services.AddTransient<AddTrackDialogViewModel>();
            services.AddTransient<AddTrackTypeDialogViewModel>();

            services.AddSingleton<GlobalViewModel>();
            services.AddSingleton<MainWindowViewModel>();
            services.AddSingleton<CarClassesDataViewModel>();
            services.AddSingleton<CarsDataViewModel>();
            services.AddSingleton<TiresDataViewModel>();
            services.AddSingleton<TracksDataViewModel>();
            services.AddSingleton<TrackTypesDataViewModel>();

            return services.BuildServiceProvider();
        }

    }
}
