using ForzaTunerResults.DataModel;
using ForzaTunerResults.ViewModel;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;

namespace ForzaTunerResults.Views;

public sealed partial class ClassResultsPage : Page
{
    private static readonly ILogger<ClassResultsPage> logger = App.CreateLogger<ClassResultsPage>();

    public ClassResultsPageViewModel ViewModel { get; }

    public ClassResultsPage()
    {
        ViewModel = App.Current.Services.GetRequiredService<ClassResultsPageViewModel>();

        InitializeComponent();
    }

    protected override async void OnNavigatedTo(NavigationEventArgs args)
    {
        base.OnNavigatedTo(args);
        var navigationParams = (NavigationParamSet)args.Parameter;

        await ViewModel.Refresh(navigationParams.TrackType, navigationParams.TrackId, navigationParams.ClassId);
    }

}
