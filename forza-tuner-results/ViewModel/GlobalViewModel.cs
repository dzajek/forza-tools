using ForzaTunerResults.DataModel;
using System.Collections.ObjectModel;

namespace ForzaTunerResults.ViewModel
{
    public sealed partial class GlobalViewModel : AbstractViewModel
    {
        private bool _isInitialized = false;
        private ObservableCollection<CarClass> _carClasses = [];
        private ObservableCollection<TrackType> _trackTypes = [];

        public bool IsInitialized
        {
            get => _isInitialized;
            set
            {
                if (_isInitialized != value)
                {
                    _isInitialized = value;
                    OnPropertyChanged(nameof(IsInitialized));
                }
            }
        }

        public ObservableCollection<CarClass> CarClasses
        {
            get => _carClasses;
            set
            {
                if (_carClasses != value)
                {
                    _carClasses = value;
                    OnPropertyChanged(nameof(CarClasses));
                }
            }
        }

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

    }
}