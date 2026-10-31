using System.Collections.ObjectModel;

namespace ForzaTunerResults.ViewModel
{
    public partial class MainWindowViewModel(GlobalViewModel globalViewModel) : AbstractViewModel
    {
        public GlobalViewModel GlobalStore { get; } = globalViewModel;

        public ObservableCollection<string> FooterMenuItems { get; } = new ObservableCollection<string>(["Data"]);
    }
}
