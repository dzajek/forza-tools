
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
    public partial class TrackTypesDataViewModel : AbstractViewModel

    {
        private static readonly ILogger<TrackTypesDataViewModel> logger = App.CreateLogger<TrackTypesDataViewModel>();
        private readonly TrackTypeService trackTypesService;
        private readonly DialogService dialogService;
        private ObservableCollection<TrackType> _trackTypes = [];

        public ICommand AddTrackTypeCommand { get; }
        public ObservableCollection<TrackType> TrackTypes
        {
            get => _trackTypes;
            set
            {
                if (_trackTypes != value)
                {
                    _trackTypes = value;
                    OnPropertyChanged(nameof(TrackTypes));
                }
            }
        }

        public TrackTypesDataViewModel(TrackTypeService trackTypesService, DialogService dialogService)
        {
            this.trackTypesService = trackTypesService;
            this.dialogService = dialogService;
            AddTrackTypeCommand = new RelayCommand(async () => await AddTrackType(), () => true);
        }

        public async Task LoadTrackTypes()
        {
            var trackTypes = await trackTypesService.LoadTrackTypesAsync();
            TrackTypes = new ObservableCollection<TrackType>(trackTypes);

            logger.LogInformation("Loaded track types: {TrackTypes}", TrackTypes);
        }

        private async Task AddTrackType()
        {
            var dialogContent = new AddTrackTypeDialog();
            ContentDialog dialog = dialogService.BuildContentDialog("Add Track Type", dialogContent);
            dialog.PrimaryButtonText = "Add";
            dialog.CloseButtonText = "Cancel";
            dialog.DefaultButton = ContentDialogButton.Primary;

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
            await LoadTrackTypes();
            logger.LogInformation("Added track type: {TrackType}", trackType);
        }
    }
}
