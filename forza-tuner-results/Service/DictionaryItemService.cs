using ForzaTunerResults.DataModel;
using ForzaTunerResults.Internal;
using ForzaTunerResults.Service.Mapper;
using ForzaTunerResults.ViewModel;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace ForzaTunerResults.Service
{
    public sealed class DictionaryItemService(DatabaseService _database)
    {
        private const string SELECT_ALL_QUERY = """
            SELECT ID, NAME
            FROM {0}
            ORDER BY ID
        """;

        private const string INSERT_SINGLE_QUERY = """
            INSERT INTO
                {0} (NAME)
                VALUES (@name)
        """;

        public Task<Collection<DictionaryItem>> LoadItemsAsync(DictionaryItemType itemType)
        {
            return Task.Run<Collection<DictionaryItem>>(() =>
            {
                using var connection = _database.OpenConnection();
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

        public Task AddItemAsync(DictionaryItemType itemType, DictionaryItem item)
        {
            return Task.Run(() =>
            {
                using var connection = _database.OpenConnection();
                using var command = connection.CreateCommand();

                command.CommandText = string.Format(INSERT_SINGLE_QUERY, itemType.TableName);
                command.Parameters.AddWithValue("@name", item.Name);

                command.ExecuteNonQuery();
            });
        }

    }
}
