using ForzaTunerResults.DataModel;
using ForzaTunerResults.DataModel.DTOs;

namespace ForzaTunerResults.ViewModel.Dialog
{
    public sealed partial class AddTuneDialogViewModel(GlobalViewModel globalViewModel) : AbstractViewModel
    {
        public GlobalViewModel GlobalStore { get; } = globalViewModel;

        private Car? _car;
        private CarClass? _selectedCarClass;
        private int _pi = 100;
        private DictionaryItem? _selectedTires;
        private DictionaryItem? _selectedSuspension;
        private DictionaryItem? _selectedDrivetrain;
        private DictionaryItem? _selectedGearbox;
        private DictionaryItem? _selectedFrontAero;
        private DictionaryItem? _selectedRearAero;
        private string? _engine;
        private double _powerToWeight;
        private int _power;
        private int _torque;
        private int _weight;
        private double _lateralGrip;
        private bool? _isDefault = false;

        public Car? Car
        {
            get => _car;
            set
            {
                if (value != _car)
                {
                    _car = value;
                    OnPropertyChanged(nameof(Car));
                }
            }
        }

        public CarClass? SelectedCarClass
        {
            get => _selectedCarClass;
            set
            {
                if (value != _selectedCarClass)
                {
                    _selectedCarClass = value;
                    OnPropertyChanged(nameof(SelectedCarClass));
                }
            }
        }

        public int Pi
        {
            get => _pi;
            set
            {
                if (value != _pi)
                {
                    _pi = value;
                    OnPropertyChanged(nameof(Pi));
                }
            }
        }

        public DictionaryItem? SelectedTires
        {
            get => _selectedTires;
            set
            {
                if (value != _selectedTires)
                {
                    _selectedTires = value;
                    OnPropertyChanged(nameof(SelectedTires));
                }
            }
        }

        public DictionaryItem? SelectedSuspension
        {
            get => _selectedSuspension;
            set
            {
                if (value != _selectedSuspension)
                {
                    _selectedSuspension = value;
                    OnPropertyChanged(nameof(SelectedSuspension));
                }
            }
        }

        public DictionaryItem? SelectedDrivetrain
        {
            get => _selectedDrivetrain;
            set
            {
                if (value != _selectedDrivetrain)
                {
                    _selectedDrivetrain = value;
                    OnPropertyChanged(nameof(SelectedDrivetrain));
                }
            }
        }

        public DictionaryItem? SelectedGearbox
        {
            get => _selectedGearbox;
            set
            {
                if (value != _selectedGearbox)
                {
                    _selectedGearbox = value;
                    OnPropertyChanged(nameof(SelectedGearbox));
                }
            }
        }

        public DictionaryItem? SelectedFrontAero
        {
            get => _selectedFrontAero;
            set
            {
                if (value != _selectedFrontAero)
                {
                    _selectedFrontAero = value;
                    OnPropertyChanged(nameof(SelectedFrontAero));
                }
            }
        }

        public DictionaryItem? SelectedRearAero
        {
            get => _selectedRearAero;
            set
            {
                if (value != _selectedRearAero)
                {
                    _selectedRearAero = value;
                    OnPropertyChanged(nameof(SelectedRearAero));
                }
            }
        }

        public string? Engine
        {
            get => _engine;
            set
            {
                if (value != _engine)
                {
                    _engine = value;
                    OnPropertyChanged(nameof(Engine));
                }
            }
        }

        public double PowerToWeight
        {
            get => _powerToWeight;
            set
            {
                if (value != _powerToWeight)
                {
                    _powerToWeight = value;
                    OnPropertyChanged(nameof(PowerToWeight));
                }
            }
        }

        public int Power
        {
            get => _power;
            set
            {
                if (value != _power)
                {
                    _power = value;
                    OnPropertyChanged(nameof(Power));
                }
            }
        }

        public int Torque
        {
            get => _torque;
            set
            {
                if (value != _torque)
                {
                    _torque = value;
                    OnPropertyChanged(nameof(Torque));
                }
            }
        }

        public int Weight
        {
            get => _weight;
            set
            {
                if (value != _weight)
                {
                    _weight = value;
                    OnPropertyChanged(nameof(Weight));
                }
            }
        }

        public double LateralGrip
        {
            get => _lateralGrip;
            set
            {
                if (value != _lateralGrip)
                {
                    _lateralGrip = value;
                    OnPropertyChanged(nameof(LateralGrip));
                }
            }
        }

        public bool? IsDefault
        {
            get => _isDefault;
            set
            {
                if (value != _isDefault)
                {
                    _isDefault = value;
                    OnPropertyChanged(nameof(IsDefault));
                }
            }
        }
    }
}
