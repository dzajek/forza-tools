using ForzaTunerResults.DataModel;
using ForzaTunerResults.Service;
using System.Threading.Tasks;

namespace ForzaTunerResults.ViewModel
{
    public sealed partial class ClassResultsPageViewModel(CarClassService carClassService) : AbstractViewModel
    {
        private CarClass? _currentCarClass;

        public CarClass? CurrentCarClass
        {
            get => _currentCarClass;
            set
            {
                if (_currentCarClass != value)
                {
                    _currentCarClass = value;
                    OnPropertyChanged(nameof(CurrentCarClass));
                }
            }
        }

        public async Task Refresh(long carClassId)
        {
            CurrentCarClass = await carClassService.LoadCarClassAsync(carClassId);
        }
    }
}
