using ForzaTunerResults.DataModel.DTOs;
using ForzaTunerResults.DataModel.Exception;
using ForzaTunerResults.Persistence;
using Microsoft.Extensions.Logging;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace ForzaTunerResults.ViewModel
{
    public sealed partial class ClassResultsPageViewModel(DatabaseQueryFactory databaseQueryFactory) : AbstractViewModel
    {
        private static readonly ILogger<ClassResultsPageViewModel> logger = App.CreateLogger<ClassResultsPageViewModel>();

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

        private Track? _currentTrack;
        public Track? CurrentTrack
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
            try
            {
                var getSingleCarClassQuery = databaseQueryFactory.GetSingleCarClassQuery(carClassId);
                CurrentCarClass = await getSingleCarClassQuery.Execute();

                var getSingleTrackQuery = databaseQueryFactory.GetSingleTrackQuery(trackTypeId, trackId);
                CurrentTrack = await getSingleTrackQuery.Execute();

                var getAllResultsQuery = databaseQueryFactory.GetResultsByTrackAndCarClassQuery(trackTypeId, trackId, carClassId);
                var results = await getAllResultsQuery.Execute();
                Results = new ObservableCollection<Result>(results);
            }
            catch (EntityNotFoundException ex)
            {
                logger.LogError(ex, "Error loading data");
                Results = new ObservableCollection<Result>([]);
                return;
            }
        }
    }
}
