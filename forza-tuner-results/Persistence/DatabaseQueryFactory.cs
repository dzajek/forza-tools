using ForzaTunerResults.DataModel.DTOs;
using ForzaTunerResults.Internal;
using ForzaTunerResults.Persistence.Queries;
using ForzaTunerResults.ViewModel;

namespace ForzaTunerResults.Persistence
{
    public sealed class DatabaseQueryFactory(DatabaseService databaseService)
    {
        public IDatabaseQuery<DictionaryItem> GetAllDictionaryItemsQuery(DictionaryItemType itemType)
        {
            return new GetAllDictionaryItemsQuery(databaseService, itemType);
        }
    }
}
