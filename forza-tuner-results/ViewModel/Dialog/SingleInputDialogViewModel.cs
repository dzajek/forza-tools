namespace ForzaTunerResults.ViewModel.Dialog
{
    public sealed partial class SingleInputDialogViewModel : AbstractViewModel
    {
        //private string? _title;
        //public string? Title
        //{
        //    get => _title;
        //    set
        //    {
        //        if (_title != value)
        //        {
        //            _title = value;
        //            OnPropertyChanged(nameof(Title));
        //        }
        //    }
        //}

        private string? _label;
        public string? Label
        {
            get => _label;
            set
            {
                if (_label != value)
                {
                    _label = value;
                    OnPropertyChanged(nameof(Label));
                }
            }
        }

        private string? _inputValue;
        public string? InputValue
        {
            get => _inputValue;
            set
            {
                if (_inputValue != value)
                {
                    _inputValue = value;
                    OnPropertyChanged(nameof(InputValue));
                }
            }
        }
    }
}
