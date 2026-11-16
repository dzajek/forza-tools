using ForzaTunerResults.DataModel;
using ForzaTunerResults.Internal;
using ForzaTunerResults.ViewModel;
using System.Threading.Tasks;

namespace ForzaTunerResults.Persistence.Commands
{
    internal sealed class CreateDictionaryItemCommand(
        DatabaseService database,
        DictionaryItemType itemType
    ) : IDatabaseCommand<DictionaryItem>
    {
        private const string INSERT_SINGLE_QUERY = """
            INSERT INTO
                {0} (NAME)
                VALUES (@name)
        """;

        public Task Execute(DictionaryItem item)
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
