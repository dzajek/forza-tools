using ForzaTunerResults.DataModel;
using ForzaTunerResults.DataModel.Entities;
using ForzaTunerResults.Internal;
using System.Threading.Tasks;

namespace ForzaTunerResults.Persistence.Commands
{
    internal sealed class CreateDictionaryItemCommand(
        DatabaseService database,
        DictionaryItemType itemType
    ) : IDatabaseCommand<DictionaryItemEntity>
    {
        private const string INSERT_SINGLE_QUERY = """
            INSERT INTO
                {0} (NAME)
                VALUES (@name)
        """;

        public Task Execute(DictionaryItemEntity item)
        {
            return Task.Run(() =>
            {
                using var connection = database.OpenConnection();
                using var command = connection.CreateCommand();

                command.CommandText = string.Format(INSERT_SINGLE_QUERY, itemType.TableName);
                command.Parameters.AddWithValue("@name", item.Name);

                command.ExecuteNonQuery();
            });
        }
    }
}
