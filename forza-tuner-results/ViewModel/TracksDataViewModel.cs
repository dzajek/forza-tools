using ForzaTunerResults.DataModel.DTOs;
using ForzaTunerResults.DataModel.Entities;
using ForzaTunerResults.Persistence;
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
        private readonly DatabaseQueryFactory _databaseQueryFactory;
        private readonly DatabaseCommandFactory _databaseCommandFactory;
        private ObservableCollection<Track> _tracks = [];

        public ObservableCollection<Track> Tracks
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

        public TracksDataViewModel(
            IDialogService dialogService,
            DatabaseQueryFactory databaseQueryFactory,
            DatabaseCommandFactory databaseCommandFactory
        )
        {
            _dialogService = dialogService;
            _databaseQueryFactory = databaseQueryFactory;
            _databaseCommandFactory = databaseCommandFactory;

            AddTrackCommand = new RelayCommand(async param => await AddTrack());
        }

        public async Task LoadTracks()
        {
            var getAllTracksQuery = _databaseQueryFactory.GetAllTracksQuery();
            var tracks = await getAllTracksQuery.Execute();
            Tracks = new ObservableCollection<Track>(tracks);
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
            var typeId = dialogContent.ViewModel.SelectedTrackType?.Id;
            var length = dialogContent.ViewModel.Length;

            if (string.IsNullOrWhiteSpace(name) || typeId is null)
            {
                logger.LogWarning("Cannot add Track. Name is null or whitespace, TypeId is invalid, or Length is invalid.");
                return;
            }

            logger.LogInformation("Adding Track with Name: {Name}", name);
            var track = new TrackEntity(0, (long)typeId, name, (double)length);
            var addTrackCommand = _databaseCommandFactory.CreateTrackCommand();
            await addTrackCommand.Execute(track);
            await LoadTracks();
            logger.LogInformation("Added track: {Track}", track);
        }
    }
}
