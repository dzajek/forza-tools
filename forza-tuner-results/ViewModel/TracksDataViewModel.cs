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
    public sealed partial class TracksDataViewModel : AbstractViewModel
    {
        private static readonly ILogger<TracksDataViewModel> logger = App.CreateLogger<TracksDataViewModel>();

        private readonly IDialogService _dialogService;
        private readonly TrackService _trackService;
        private ObservableCollection<TrackDescription> _tracks = [];

        public ObservableCollection<TrackDescription> Tracks
        {
            get => _tracks;
            set
            {
                if (_tracks != value)
                {
                    _tracks = value;
                    OnPropertyChanged(nameof(Tracks));
                }
            }
        }

        public ICommand AddTrackCommand { get; }

        public TracksDataViewModel(IDialogService dialogService, TrackService trackService)
        {
            _dialogService = dialogService;
            _trackService = trackService;

            AddTrackCommand = new RelayCommand(async param => await AddTrack(), () => true);
        }

        public async Task LoadTracks()
        {
            var tracks = await _trackService.LoadAllTracks();
            Tracks = new ObservableCollection<TrackDescription>(tracks);
            logger.LogInformation("Loaded tracks: {Tracks}", Tracks);
        }

        private async Task AddTrack()
        {
            var dialogContent = new AddTrackDialog();

            var options = new ContentDialogOptions(
                Title: "Add Track",
                PrimaryButtonText: "Add",
                CloseButtonText: "Cancel",
                Content: dialogContent
            );

            bool dialogApproved = await _dialogService.ShowContentDialog(options);

            if (!dialogApproved)
            {
                logger.LogInformation("Add Track dialog was canceled.");
                return;
            }

            var name = dialogContent.ViewModel.Name;
            var typeId = dialogContent.ViewModel.SelectedTrackTypeId;
            var length = dialogContent.ViewModel.Length;

            if (string.IsNullOrWhiteSpace(name) || typeId is null)
            {
                logger.LogWarning("Cannot add Track. Name is null or whitespace, TypeId is invalid, or Length is invalid.");
                return;
            }

            logger.LogInformation("Adding Track with Name: {Name}", name);
            var track = new Track(0, (long)typeId, name, (float)length);
            await _trackService.AddTrackAsync(track);
            await LoadTracks();
            logger.LogInformation("Added track: {Track}", track);
        }
    }
}
