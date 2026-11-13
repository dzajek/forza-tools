using ForzaTunerResults.DataModel;
using ForzaTunerResults.Service;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace ForzaTunerResults.ViewModel
{
    public sealed partial class ClassResultsPageViewModel(
        CarClassService carClassService,
        TrackService trackService,
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

        private TrackDescription? _currentTrack;
        public TrackDescription? CurrentTrack
        {
            get => _currentTrack;
            set
            {
                if (_currentTrack != value)
                {
                    _currentTrack = value;
                    OnPropertyChanged(nameof(CurrentTrack));
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

        public async Task Refresh(long trackTypeId, long trackId, long carClassId)
        {
            CurrentCarClass = await carClassService.LoadCarClassAsync(carClassId);
            CurrentTrack = await trackService.LoadTrackAsync(trackTypeId, trackId);
            if (CurrentCarClass is not null && CurrentTrack is not null)
            {
                var results = await resultService.LoadResultsAsync(trackTypeId, CurrentTrack.Id, CurrentCarClass.Id);
                Results = new ObservableCollection<Result>(results);
            }
            else
            {
                Results = new ObservableCollection<Result>([]);
            }
        }
    }
}
