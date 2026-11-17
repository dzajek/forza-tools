using ForzaTunerResults.DataModel;
using ForzaTunerResults.DataModel.DTOs;
using ForzaTunerResults.Internal;
using ForzaTunerResults.Persistence.Queries;
using System.Collections.ObjectModel;

namespace ForzaTunerResults.Persistence
{
    public sealed class DatabaseQueryFactory(DatabaseService databaseService)
    {
        public IDatabaseQuery<Collection<DictionaryItem>> GetDictionaryItemsByTypeQuery(DictionaryItemType itemType) => new GetDictionaryItemsByTypeQuery(databaseService, itemType);

        public IDatabaseQuery<Collection<CarClass>> GetAllCarClassesQuery() => new GetAllCarClassesQuery(databaseService);

        public IDatabaseQuery<CarClass> GetSingleCarClassQuery(long carClassId) => new GetSingleCarClassQuery(databaseService, carClassId);

        public IDatabaseQuery<Collection<Track>> GetAllTracksQuery() => new GetAllTracksQuery(databaseService);

        public IDatabaseQuery<Track> GetSingleTrackQuery(long trackTypeId, long trackId) => new GetSingleTrackQuery(databaseService, trackTypeId, trackId);

        public IDatabaseQuery<Collection<Car>> GetAllCarsQuery() => new GetAllCarsQuery(databaseService);

        public IDatabaseQuery<Collection<Tune>> GetTunesByCarQuery(long carId) => new GetTunesByCarQuery(databaseService, carId);

        public IDatabaseQuery<Collection<Result>> GetResultsByTrackAndCarClassQuery(long trackTypeId, long trackId, long carClassId) => new GetResultsByTrackAndCarClassQuery(databaseService, trackTypeId, trackId, carClassId);
    }
}
