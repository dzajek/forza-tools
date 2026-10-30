using ForzaTunerResults.ViewModel.Dialog;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.UI.Xaml.Controls;

namespace ForzaTunerResults.Views.Dialog
{

    public sealed partial class AddTrackDialog : Page
    {
        private static readonly ILogger<AddTrackDialog> logger = App.CreateLogger<AddTrackDialog>();

        public AddTrackDialogViewModel ViewModel { get; }

        public AddTrackDialog()
        {
            ViewModel = App.Current.Services.GetRequiredService<AddTrackDialogViewModel>();

            InitializeComponent();
        }

        internal void Page_Loaded(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {
            if (TrackTypeMenuFlyout is not null)
            {
                TrackTypeMenuFlyout.Items.Clear();
                foreach (var trackType in ViewModel.GlobalStore.TrackTypes)
                {
                    var menuItem = new MenuFlyoutItem
                    {
                        Text = trackType.Name,
                        Tag = trackType.Id
                    };
                    menuItem.Click += (s, e1) => ViewModel.SelectedTrackTypeId = (long)((MenuFlyoutItem)s).Tag;

                    TrackTypeMenuFlyout.Items.Add(menuItem);
                }
            }
            else
            {
                logger.LogWarning("TrackTypeMenuFlyout is null, cannot add menu items");
            }
        }
    }
}
