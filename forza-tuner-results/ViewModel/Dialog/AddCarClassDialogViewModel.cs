namespace ForzaTunerResults.ViewModel.Dialog
{
    public sealed partial class AddCarClassDialogViewModel : AbstractViewModel
    {
        private string? prefix;
        public string? Prefix
        {
            get => prefix;
            set
            {
                if (prefix != value)
                {
                    prefix = value;
                    OnPropertyChanged(nameof(Prefix));
                }
            }
        }

        private int? piLimit;
        public int? PiLimit
        {
            get => piLimit;
            set
            {
                if (piLimit != value)
                {
                    piLimit = value;
                    OnPropertyChanged(nameof(PiLimit));
                }
            }
        }
    }
}
