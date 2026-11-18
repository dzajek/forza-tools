using ForzaTunerResults.Persistence;
using ForzaTunerResults.ViewModel;
using ForzaTunerResults.ViewModel.Dialog;
using ForzaTunerResults.ViewModel.Utils;
using ForzaTunerResults.Views.Utils;
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
            services.AddSingleton<DatabaseQueryFactory>();
            services.AddSingleton<DatabaseCommandFactory>();

            services.AddSingleton<DialogService, DialogService>();
            services.AddSingleton<IDialogService, DialogService>(provider => provider.GetRequiredService<DialogService>());

            // Global Store
            services.AddSingleton<GlobalViewModel>(GlobalViewModelInitializationFactory.InitializeGlobalViewModel);

            // Dialog ViewModels
            services.AddTransient<SingleInputDialogViewModel>();
            services.AddTransient<AddCarDialogViewModel>();
            services.AddTransient<AddCarClassDialogViewModel>();
            services.AddTransient<AddTrackDialogViewModel>();
            services.AddTransient<AddTuneDialogViewModel>();

            // Page ViewModels
            services.AddTransient<MainWindowViewModel>();
            services.AddTransient<CarClassesDataViewModel>();
            services.AddTransient<CarsDataViewModel>();
            services.AddTransient<TracksDataViewModel>();
            services.AddTransient<DictionariesPageViewModel>();
            services.AddTransient<ClassResultsPageViewModel>();

            return services.BuildServiceProvider();
        }
    }
}
