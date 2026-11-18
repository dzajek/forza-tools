namespace ForzaTunerResults.ViewModel.Utils
{
    public record ContentDialogOptions<D, M>(
        string Title,
        string PrimaryButtonText,
        string CloseButtonText,
        D Content
    ) where D : IDialogWithModel<M> where M : class;
}
