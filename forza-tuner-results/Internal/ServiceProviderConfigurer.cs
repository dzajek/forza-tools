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

            services.AddSingleton<DictionaryItemService>();

            services.AddSingleton<CarClassService>();
            services.AddSingleton<CarService>();
            services.AddSingleton<TrackService>();

            services.AddSingleton<GlobalStoreService>();

            // Dialog ViewModels
            services.AddTransient<SingleInputDialogViewModel>();
            services.AddTransient<AddCarDialogViewModel>();
            services.AddTransient<AddCarClassDialogViewModel>();
            services.AddTransient<AddTrackDialogViewModel>();

            services.AddSingleton<GlobalViewModel>();

            // Page ViewModels
            services.AddTransient<MainWindowViewModel>();
            services.AddTransient<CarClassesDataViewModel>();
            services.AddTransient<CarsDataViewModel>();
            services.AddTransient<TracksDataViewModel>();
            services.AddTransient<OthersPageViewModel>();

            return services.BuildServiceProvider();
        }

    }
}
