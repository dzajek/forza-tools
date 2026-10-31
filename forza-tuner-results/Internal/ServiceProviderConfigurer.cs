using ForzaTunerResults.Service;
using ForzaTunerResults.ViewModel;
using ForzaTunerResults.ViewModel.Dialog;
using ForzaTunerResults.Views.ViewUtils;
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
            services.AddSingleton<IDialogService>(new DialogService());

            services.AddSingleton<CarClassService>();
            services.AddSingleton<CarService>();
            services.AddSingleton<TiresService>();
            services.AddSingleton<TrackService>();
            services.AddSingleton<TrackTypeService>();

            services.AddSingleton<GlobalStoreService>();

            // Dialog ViewModels
            services.AddTransient<AddCarDialogViewModel>();
            services.AddTransient<AddCarClassDialogViewModel>();
            services.AddTransient<AddTiresDialogViewModel>();
            services.AddTransient<AddTrackDialogViewModel>();
            services.AddTransient<AddTrackTypeDialogViewModel>();

            services.AddSingleton<GlobalViewModel>();

            // Page ViewModels
            services.AddTransient<MainWindowViewModel>();
            services.AddTransient<CarClassesDataViewModel>();
            services.AddTransient<CarsDataViewModel>();
            services.AddTransient<TiresDataViewModel>();
            services.AddTransient<TracksDataViewModel>();
            services.AddTransient<TrackTypesDataViewModel>();

            return services.BuildServiceProvider();
        }

    }
}
