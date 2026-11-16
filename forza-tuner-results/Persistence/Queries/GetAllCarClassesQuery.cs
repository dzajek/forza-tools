using ForzaTunerResults.DataModel.DTOs;
using ForzaTunerResults.Internal;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace ForzaTunerResults.Persistence.Queries
{
    internal class GetAllCarClassesQuery(DatabaseService database) : IDatabaseQuery<Collection<CarClass>>
    {
        private const string SELECT_ALL_QUERY = """
            SELECT ID, PREFIX, PI_LIMIT
            FROM CAR_CLASS
            ORDER BY ID
        """;
        public Task<Collection<CarClass>> Execute()
        {
            return Task.Run<Collection<CarClass>>(() =>
            {
                using var connection = database.OpenConnection();
                using var command = connection.CreateCommand();
                command.CommandText = SELECT_ALL_QUERY;
                using var reader = command.ExecuteReader();
                var items = new Collection<CarClass>();
                while (reader.Read())
                {
                    items.Add(new CarClass(
                        reader.GetInt64(0),
                        reader.GetString(1),
                        reader.GetInt32(2)
                    ));
                }
                return items;
            });
        }
    }
}
