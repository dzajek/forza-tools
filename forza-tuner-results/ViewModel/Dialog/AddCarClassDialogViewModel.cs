namespace ForzaTunerResults.ViewModel.Dialog
{
    public sealed partial class AddCarClassDialogViewModel : AbstractViewModel
    {

        private string? letter;
        public string? Letter
        {
            get => letter;
            set
            {
                if (letter != value)
                {
                    letter = value;
                    OnPropertyChanged(nameof(Letter));
                }
            }
        }

        private int? piLImit;
        public int? PiLimit
        {
            get => piLImit;
            set
            {
                if (piLImit != value)
                {
                    piLImit = value;
                    OnPropertyChanged(nameof(PiLimit));
                }
            }
        }

    }
}
