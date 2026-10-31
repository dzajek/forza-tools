using ForzaTunerResults.DataModel;
using ForzaTunerResults.Internal;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace ForzaTunerResults.Service
{
    public sealed class CarClassService(DatabaseService _database)
    {
        private const string SELECT_ALL_QUERY = """
            SELECT ID, LETTER, PI_LIMIT
            FROM CAR_CLASS
            ORDER BY PI_LIMIT ASC
        """;

        private const string INSERT_SINGLE_QUERY = """
            INSERT INTO
                CAR_CLASS (LETTER, PI_LIMIT)
                VALUES (@letter, @piLimit)
        """;

        public Task<Collection<CarClass>> LoadCarClassesAsync()
        {
            return Task.Run<Collection<CarClass>>(() =>
            {
                using var connection = _database.OpenConnection();
                using var command = connection.CreateCommand();

                command.CommandText = SELECT_ALL_QUERY;

                using var reader = command.ExecuteReader();

                var carClasses = new Collection<CarClass>();
                while (reader.Read())
                {
                    var id = reader.GetInt32(0);
                    var letter = reader.GetString(1);
                    var piLimit = reader.GetInt32(2);

                    carClasses.Add(new(id, letter, piLimit));
                }

                return carClasses;
            });
        }

        public Task AddCarClassAsync(CarClass carClass)
        {
            return Task.Run(() =>
            {
                using var connection = _database.OpenConnection();
                using var command = connection.CreateCommand();

                command.CommandText = INSERT_SINGLE_QUERY;
                command.Parameters.AddWithValue("@letter", carClass.Letter);
                command.Parameters.AddWithValue("@piLimit", carClass.PiLimit);

                command.ExecuteNonQuery();
            });
        }
    }
}
