using ForzaTunerResults.DataModel;
using ForzaTunerResults.Internal;
using ForzaTunerResults.Service;
using ForzaTunerResults.ViewModel.Commands;
using ForzaTunerResults.Views.Dialog;
using Microsoft.Extensions.Logging;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ForzaTunerResults.ViewModel
{
    public sealed partial class TracksDataViewModel : AbstractViewModel
    {

        private static readonly ILogger<TracksDataViewModel> logger = App.CreateLogger<TracksDataViewModel>();
        private readonly TrackService trackService;
        private readonly DialogService dialogService;
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

        public TracksDataViewModel(TrackService trackService, DialogService dialogService)
        {
            this.trackService = trackService;
            this.dialogService = dialogService;
            AddTrackCommand = new RelayCommand(async () => await AddTrack(), () => true);
        }

        public async Task LoadTracks()
        {
            var tracks = await trackService.LoadAllTracks();
            Tracks = new ObservableCollection<TrackDescription>(tracks);
            logger.LogInformation("Loaded tracks: {Tracks}", Tracks);
        }

        private async Task AddTrack()
        {
            var dialogContent = new AddTrackDialog();
            ContentDialog dialog = dialogService.BuildContentDialog("Add Track", dialogContent);
            dialog.PrimaryButtonText = "Add";
            dialog.CloseButtonText = "Cancel";
            dialog.DefaultButton = ContentDialogButton.Primary;

            var result = await dialog.ShowAsync();
            if (result != ContentDialogResult.Primary)
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
            await trackService.AddTrackAsync(track);
            await LoadTracks();
            logger.LogInformation("Added track: {Track}", track);
        }
    }
}
