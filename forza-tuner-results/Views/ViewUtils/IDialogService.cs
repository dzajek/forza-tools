using Microsoft.UI.Xaml;
using System.Threading.Tasks;

namespace ForzaTunerResults.Views.ViewUtils
{
    public interface IDialogService
    {
        XamlRoot XamlRoot { get; set; }

        public Task<bool> ShowContentDialog(ContentDialogOptions options);
    }
}
