using ForzaTunerResults.ViewModel;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using System;

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

        await ViewModel.Refresh(Convert.ToInt64(args.Parameter));
    }

}
