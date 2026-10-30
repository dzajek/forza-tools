
using ForzaTunerResults.DataModel;
using ForzaTunerResults.Service;
using ForzaTunerResults.ViewModel.Commands;
using ForzaTunerResults.Views.Dialog;
using Microsoft.Extensions.Logging;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ForzaTunerResults.ViewModel
{
    public partial class TrackTypesDataViewModel : AbstractViewModel

    {
        private static readonly ILogger<TrackTypesDataViewModel> logger = App.CreateLogger<TrackTypesDataViewModel>();

        private readonly TrackTypeService trackTypesService;

        public GlobalViewModel GlobalState;

        public ICommand AddTrackTypeCommand { get; }

        public TrackTypesDataViewModel(GlobalViewModel globalViewModel, TrackTypeService trackTypesService)
        {
            GlobalState = globalViewModel;
            this.trackTypesService = trackTypesService;

            AddTrackTypeCommand = new DialogBasedCommand(async parent => await AddTrackType(parent));
        }

        private async Task AddTrackType(UIElement parent)
        {
            var dialogContent = new AddTrackTypeDialog();
            ContentDialog dialog = new()
            {
                XamlRoot = parent.XamlRoot,
                Title = "Add Track Type",
                Content = dialogContent,
                PrimaryButtonText = "Add",
                CloseButtonText = "Cancel",
                DefaultButton = ContentDialogButton.Primary
            };

            var result = await dialog.ShowAsync();
            if (result != ContentDialogResult.Primary)
            {
                logger.LogInformation("Add Track Type dialog was canceled.");
                return;
            }

            var name = dialogContent.ViewModel.Name;

            if (string.IsNullOrWhiteSpace(name))
            {
                logger.LogWarning("Cannot add Track Type. Name is null or whitespace.");
                return;
            }

            logger.LogInformation("Adding Track Type with Name: {Name}", name);

            var trackType = new TrackType(0, name);
            await trackTypesService.AddTrackTypeAsync(trackType);
            var trackTypes = await trackTypesService.LoadTrackTypesAsync();
            GlobalState.TrackTypes = new ObservableCollection<TrackType>(trackTypes);

            logger.LogInformation("Added track type: {TrackType}", trackType);
        }
    }
}
