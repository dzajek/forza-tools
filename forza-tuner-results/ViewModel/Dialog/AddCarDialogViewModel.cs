namespace ForzaTunerResults.ViewModel.Dialog
{
    public sealed partial class AddCarDialogViewModel : AbstractViewModel
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
    }
}
