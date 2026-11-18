using ForzaTunerResults.DataModel.DTOs;
using ForzaTunerResults.ViewModel.Dialog;
using System.Threading.Tasks;

namespace ForzaTunerResults.ViewModel.Utils
{
    public interface IDialogService
    {
        Task<AddCarClassDialogViewModel?> ShowAddCarClassDialog();
        Task<AddCarDialogViewModel?> ShowAddCarDialog();
        Task<AddTrackDialogViewModel?> ShowAddTrackDialog();
        Task<AddTuneDialogViewModel?> ShowAddTuneDialog(Car car);
        Task<SingleInputDialogViewModel?> ShowSingleInputDialog(string title, string label);
    }
}
