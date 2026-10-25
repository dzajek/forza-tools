

using System.ComponentModel;
using System.Diagnostics;

namespace ForzaTunerResults.ViewModel.Dialog
{
    public partial class AddCarDialogViewModel : INotifyPropertyChanged
    {
        private int? year = 2000;

        public int? Year
        {
            get => year;
            set
            {
                if (year != value)
                {
                    year = value;
                    OnPropertyChanged(nameof(Year));
                }
            }
        }

        private string? manufacturer;

        public string? Manufacturer
        {
            get => manufacturer;
            set
            {
                if (manufacturer != value)
                {
                    manufacturer = value;
                    OnPropertyChanged(nameof(Manufacturer));
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

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
