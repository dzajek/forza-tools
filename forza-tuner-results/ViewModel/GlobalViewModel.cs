using ForzaTunerResults.DataModel;
using System;
using System.Collections.ObjectModel;

namespace ForzaTunerResults.ViewModel
{
    public sealed partial class GlobalViewModel : AbstractViewModel
    {
        private bool _isInitialized = false;
        private ObservableCollection<CarClass> _carClasses = [];
        private ObservableCollection<DictionaryItem> _tires = [];
        private ObservableCollection<DictionaryItem> _trackTypes = [];
        private ObservableCollection<DictionaryItem> _suspensions = [];
        private ObservableCollection<DictionaryItem> _drivetrains = [];
        private ObservableCollection<DictionaryItem> _gearboxes = [];
        private ObservableCollection<DictionaryItem> _aeros = [];

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

        public ObservableCollection<DictionaryItem> Tires
        {
            get => _tires;
            set
            {
                if (_tires != value)
                {
                    _tires = value;
                    OnPropertyChanged(nameof(Tires));
                }
            }
        }

        public ObservableCollection<DictionaryItem> TrackTypes
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

        public ObservableCollection<DictionaryItem> Suspensions
        {
            get => _suspensions;
            set
            {
                if (_suspensions != value)
                {
                    _suspensions = value;
                    OnPropertyChanged(nameof(Suspensions));
                }
            }
        }

        public ObservableCollection<DictionaryItem> Drivetrains
        {
            get => _drivetrains;
            set
            {
                if (_drivetrains != value)
                {
                    _drivetrains = value;
                    OnPropertyChanged(nameof(Drivetrains));
                }
            }
        }

        public ObservableCollection<DictionaryItem> Gearboxes
        {
            get => _gearboxes;
            set
            {
                if (_gearboxes != value)
                {
                    _gearboxes = value;
                    OnPropertyChanged(nameof(Gearboxes));
                }
            }
        }

        public ObservableCollection<DictionaryItem> Aeros
        {
            get => _aeros;
            set
            {
                if (_aeros != value)
                {
                    _aeros = value;
                    OnPropertyChanged(nameof(Aeros));
                }
            }
        }

        internal void RefreshDictionaryItems(DictionaryItemType itemType, Collection<DictionaryItem> items)
        {
            switch (itemType.Key)
            {
                case DictionaryItemEnum.TIRES:
                    Tires = new ObservableCollection<DictionaryItem>(items);
                    break;
                case DictionaryItemEnum.SUSPENSION:
                    Suspensions = new ObservableCollection<DictionaryItem>(items);
                    break;
                case DictionaryItemEnum.DRIVETRAIN:
                    Drivetrains = new ObservableCollection<DictionaryItem>(items);
                    break;
                case DictionaryItemEnum.GEARBOX:
                    Gearboxes = new ObservableCollection<DictionaryItem>(items);
                    break;
                case DictionaryItemEnum.AERO:
                    Aeros = new ObservableCollection<DictionaryItem>(items);
                    break;
                case DictionaryItemEnum.TRACKTYPE:
                    TrackTypes = new ObservableCollection<DictionaryItem>(items);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(itemType), $"Unhandled dictionary item type: {itemType.Key}");
            }
        }
    }
}