
using ForzaTunerResults.DataModel;
using ForzaTunerResults.Service;
using Microsoft.Extensions.Logging;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace ForzaTunerResults.ViewModel.Dialog
{
    public sealed partial class AddTrackDialogViewModel(GlobalViewModel globalViewModel, TrackTypeService trackTypeService) : AbstractViewModel
    {
        private static readonly ILogger<AddTrackDialogViewModel> logger = App.CreateLogger<AddTrackDialogViewModel>();

        public GlobalViewModel GlobalStore { get; } = globalViewModel;

        private string? name;
        public string? Name
        {
            get => name;
            set
            {
                if (name != value)
                {
                    name = value;
                    OnPropertyChanged(nameof(Name));
                }
            }
        }

        private double length;
        public double Length
        {
            get => length;
            set
            {
                if (length != value)
                {
                    length = value;
                    OnPropertyChanged(nameof(Length));
                }
            }
        }

        private long? selectedTrackTypeId;
        public long? SelectedTrackTypeId
        {
            get => selectedTrackTypeId;
            set
            {
                if (selectedTrackTypeId != value)
                {
                    selectedTrackTypeId = value;
                    OnPropertyChanged(nameof(SelectedTrackTypeId));
                    OnPropertyChanged(nameof(SelectedTrackTypeName));
                }
            }
        }

        public string? SelectedTrackTypeName
        {
            get
            {
                var trackType = GlobalStore.TrackTypes.FirstOrDefault(tt => tt.Id == SelectedTrackTypeId);
                return trackType is null ? "Track type" : trackType.Name;
            }
        }

        public async Task LoadTrackTypes()
        {
            var trackTypes = await trackTypeService.LoadTrackTypesAsync();
            GlobalStore.TrackTypes = new ObservableCollection<TrackType>(trackTypes);
        }

    }
}
