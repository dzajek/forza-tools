using ForzaTunerResults.DataModel.Entities;
using ForzaTunerResults.Internal;
using ForzaTunerResults.Persistence.Commands;
using ForzaTunerResults.ViewModel;

namespace ForzaTunerResults.Persistence
{
    public sealed class DatabaseCommandFactory(DatabaseService databaseService)
    {
        public IDatabaseCommand<DictionaryItemEntity> CreateDictionaryItemCommand(DictionaryItemType itemType)
        {
            return new CreateDictionaryItemCommand(databaseService, itemType);
        }
    }
}
