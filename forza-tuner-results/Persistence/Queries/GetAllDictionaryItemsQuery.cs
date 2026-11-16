using ForzaTunerResults.DataModel.DTOs;
using ForzaTunerResults.Internal;
using ForzaTunerResults.Service.Mapper;
using ForzaTunerResults.ViewModel;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace ForzaTunerResults.Persistence.Queries
{
    internal class GetAllDictionaryItemsQuery(
        DatabaseService database,
        DictionaryItemType itemType
    ) : IDatabaseQuery<Collection<DictionaryItem>>
    {
        private const string SELECT_ALL_QUERY = """
            SELECT ID, NAME
            FROM {0}
            ORDER BY ID
        """;

        public Task<Collection<DictionaryItem>> Execute()
        {
            return Task.Run<Collection<DictionaryItem>>(() =>
            {
                using var connection = database.OpenConnection();
                using var command = connection.CreateCommand();

                command.CommandText = string.Format(SELECT_ALL_QUERY, itemType.TableName);

                using var reader = command.ExecuteReader();

                var mapper = DictionaryItemMapper.Initialize(reader);
                var items = new Collection<DictionaryItem>();
                while (reader.Read())
                {
                    items.Add(mapper.MapFromRow(reader));
                }

                return items;
            });
        }
    }
}
