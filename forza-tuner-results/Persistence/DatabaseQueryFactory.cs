using ForzaTunerResults.DataModel.DTOs;
using ForzaTunerResults.Internal;
using ForzaTunerResults.Persistence.Queries;
using ForzaTunerResults.ViewModel;
using System.Collections.ObjectModel;

namespace ForzaTunerResults.Persistence
{
    public sealed class DatabaseQueryFactory(DatabaseService databaseService)
    {
        public IDatabaseQuery<Collection<DictionaryItem>> GetAllDictionaryItemsQuery(DictionaryItemType itemType) => new GetAllDictionaryItemsQuery(databaseService, itemType);

        public IDatabaseQuery<Collection<CarClass>> GetAllCarClassesQuery() => new GetAllCarClassesQuery(databaseService);

        public IDatabaseQuery<CarClass> GetSingleCarClassQuery(long carClassId) => new GetSingleCarClassQuery(databaseService, carClassId);

        public IDatabaseQuery<Collection<Track>> GetAllTracksQuery() => new GetAllTracksQuery(databaseService);

        public IDatabaseQuery<Track> GetSingleTrackQuery(long trackTypeId, long trackId) => new GetSingleTrackQuery(databaseService, trackTypeId, trackId);
    }
}
