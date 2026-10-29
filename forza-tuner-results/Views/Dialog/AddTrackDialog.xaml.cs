using ForzaTunerResults.ViewModel.Dialog;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System.Threading.Tasks;

namespace ForzaTunerResults.Views.Dialog
{

    public sealed partial class AddTrackDialog : Page
    {
        private static readonly ILogger<AddTrackDialog> logger = App.CreateLogger<AddTrackDialog>();

        public AddTrackDialogViewModel ViewModel { get; }

        public AddTrackDialog()
        {
            InitializeComponent();
            ViewModel = App.Current.Services.GetRequiredService<AddTrackDialogViewModel>();
            ViewModel.LoadTrackTypes().ContinueWith(task =>
            {
                if (task.IsCompletedSuccessfully)
                {
                    foreach (var trackType in ViewModel.TrackTypes)
                    {
                        var menuItem = new MenuFlyoutItem
                        {
                            Text = trackType.Name,
                            Tag = trackType.Id
                        };
                        menuItem.Click += (object s, RoutedEventArgs e1) =>
                        {
                            ViewModel.SelectedTrackTypeId = (long)((MenuFlyoutItem)s).Tag;
                        };

                        TrackTypeMenuFlyout.Items.Add(menuItem);
                    }
                }
                else if (task.IsFaulted)
                {
                    logger.LogError(task.Exception, "Failed to load track types.");
                }
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }
    }
}
