namespace ForzaTunerResults.ViewModel.Utils
{
    public interface IDialogWithModel<T> where T : class
    {
        public T ViewModel { get; }
    }
}
