using ForzaTunerResults.DataModel.Entities;
using ForzaTunerResults.Internal;
using ForzaTunerResults.Persistence.Commands;
using ForzaTunerResults.ViewModel;

namespace ForzaTunerResults.Persistence
{
    public sealed class DatabaseCommandFactory(DatabaseService databaseService)
    {
        public IDatabaseCommand<DictionaryItemEntity> CreateDictionaryItemCommand(DictionaryItemType itemType) => new CreateDictionaryItemCommand(databaseService, itemType);

        public IDatabaseCommand<CarClassEntity> CreateCarClassCommand() => new CreateCarClassCommand(databaseService);

        public IDatabaseCommand<TrackEntity> CreateTrackCommand() => new CreateTrackCommand(databaseService);
    }
}
