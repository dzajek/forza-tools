using ForzaTunerResults.DataModel;
using ForzaTunerResults.Service;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace ForzaTunerResults.ViewModel
{
    public sealed partial class ClassResultsPageViewModel(
        CarClassService carClassService,
        ResultService resultService
    ) : AbstractViewModel
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

        private ObservableCollection<Result> _results = [];
        public ObservableCollection<Result> Results
        {
            get => _results;
            set
            {
                if (_results != value)
                {
                    _results = value;
                    OnPropertyChanged(nameof(Results));
                }
            }
        }

        public async Task Refresh(long carClassId)
        {
            CurrentCarClass = await carClassService.LoadCarClassAsync(carClassId);
            if (CurrentCarClass is not null)
            {
                var results = await resultService.LoadResultsAsync(CurrentCarClass.Id);
                Results = new ObservableCollection<Result>(results);
            }
            else
            {
                Results = new ObservableCollection<Result>([]);
            }
        }
    }
}
