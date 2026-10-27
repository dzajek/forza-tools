namespace ForzaTunerResults.ViewModel.Dialog
{
    public sealed partial class AddTiresDialogViewModel : AbstractViewModel
    {
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
