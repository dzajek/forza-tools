
using ForzaTunerResults.DataModel;
using ForzaTunerResults.Service;
using ForzaTunerResults.ViewModel.Commands;
using ForzaTunerResults.Views.Dialog;
using ForzaTunerResults.Views.ViewUtils;
using Microsoft.Extensions.Logging;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ForzaTunerResults.ViewModel
{
    public partial class TrackTypesDataViewModel : AbstractViewModel
    {
        private static readonly ILogger<TrackTypesDataViewModel> logger = App.CreateLogger<TrackTypesDataViewModel>();

        private readonly IDialogService _dialogService;
        private readonly TrackTypeService _trackTypesService;

        public GlobalViewModel GlobalState;
        public ICommand AddTrackTypeCommand { get; }

        public TrackTypesDataViewModel(
            IDialogService dialogService,
            TrackTypeService trackTypesService,
            GlobalViewModel globalViewModel)
        {
            _dialogService = dialogService;
            _trackTypesService = trackTypesService;
            GlobalState = globalViewModel;

            AddTrackTypeCommand = new RelayCommand(async param => await AddTrackType(), () => true);
        }

        private async Task AddTrackType()
        {
            var dialogContent = new AddTrackTypeDialog();

            var options = new ContentDialogOptions(
                Title: "Add Track Type",
                PrimaryButtonText: "Add",
                CloseButtonText: "Cancel",
                Content: dialogContent
            );

            bool dialogApproved = await _dialogService.ShowContentDialog(options);

            if (!dialogApproved)
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
            await _trackTypesService.AddTrackTypeAsync(trackType);
            var trackTypes = await _trackTypesService.LoadTrackTypesAsync();
            GlobalState.TrackTypes = new ObservableCollection<TrackType>(trackTypes);

            logger.LogInformation("Added track type: {TrackType}", trackType);
        }
    }
}
