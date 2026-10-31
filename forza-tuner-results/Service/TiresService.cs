using ForzaTunerResults.DataModel;
using ForzaTunerResults.Internal;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace ForzaTunerResults.Service
{
    public sealed class TiresService(DatabaseService _database)
    {
        private const string SELECT_ALL_QUERY = """
            SELECT ID, NAME
            FROM TIRES
            ORDER BY ID
        """;

        private const string INSERT_SINGLE_QUERY = """
            INSERT INTO
                TIRES (NAME)
                VALUES (@name)
        """;

        public Task<Collection<Tires>> LoadTiresAsync()
        {
            return Task.Run<Collection<Tires>>(() =>
            {
                using var connection = _database.OpenConnection();
                using var command = connection.CreateCommand();

                command.CommandText = SELECT_ALL_QUERY;

                using var reader = command.ExecuteReader();

                var tires = new Collection<Tires>();
                while (reader.Read())
                {
                    var id = reader.GetInt32(0);
                    var name = reader.GetString(1);
                    tires.Add(new(id, name));
                }

                return tires;
            });
        }

        public Task AddTireAsync(Tires tires)
        {
            return Task.Run(() =>
            {
                using var connection = _database.OpenConnection();
                using var command = connection.CreateCommand();

                command.CommandText = INSERT_SINGLE_QUERY;
                command.Parameters.AddWithValue("@name", tires.Name);

                command.ExecuteNonQuery();
            });
        }
    }
}
