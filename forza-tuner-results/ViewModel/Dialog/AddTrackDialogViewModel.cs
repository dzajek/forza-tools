
using ForzaTunerResults.DataModel;
using ForzaTunerResults.Service;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace ForzaTunerResults.ViewModel.Dialog
{
    public sealed partial class AddTrackDialogViewModel(GlobalViewModel globalViewModel, DictionaryItemService dictionaryItemService) : AbstractViewModel
    {
        public GlobalViewModel GlobalStore { get; } = globalViewModel;

        private DictionaryItem? _selectedTrackType;
        public DictionaryItem? SelectedTrackType
        {
            get => _selectedTrackType;
            set
            {
                if (value != _selectedTrackType)
                {
                    _selectedTrackType = value;
                    OnPropertyChanged(nameof(SelectedTrackType));
                }
            }
        }

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

        public async Task LoadTrackTypes()
        {
            var trackTypes = await dictionaryItemService.LoadItemsAsync(DictionaryItemType.TrackType);
            GlobalStore.TrackTypes = new ObservableCollection<DictionaryItem>(trackTypes);
        }

    }
}
