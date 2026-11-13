using ForzaTunerResults.DataModel;
using ForzaTunerResults.Internal;
using ForzaTunerResults.Service.Mapper;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace ForzaTunerResults.Service
{
    public sealed class CarClassService(DatabaseService _database)
    {
        private const string SELECT_ALL_QUERY = """
            SELECT ID, PREFIX, PI_LIMIT
            FROM CAR_CLASS
            ORDER BY PI_LIMIT ASC
        """;

        private const string SELECT_SINGLE_QUERY = """
            SELECT ID, PREFIX, PI_LIMIT
            FROM CAR_CLASS
            WHERE ID = @id
        """;

        private const string INSERT_SINGLE_QUERY = """
            INSERT INTO
                CAR_CLASS (PREFIX, PI_LIMIT)
                VALUES (@prefix, @piLimit)
        """;

        public Task<Collection<CarClass>> LoadCarClassesAsync()
        {
            return Task.Run<Collection<CarClass>>(() =>
            {
                using var connection = _database.OpenConnection();
                using var command = connection.CreateCommand();

                command.CommandText = SELECT_ALL_QUERY;

                using var reader = command.ExecuteReader();

                var mapper = CarClassMapper.Initialize(reader);
                var carClasses = new Collection<CarClass>();
                while (reader.Read())
                {
                    carClasses.Add(mapper.MapFromRow(reader));
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
                command.Parameters.AddWithValue("@prefix", carClass.Prefix);
                command.Parameters.AddWithValue("@piLimit", carClass.PiLimit);

                command.ExecuteNonQuery();
            });
        }

        public Task<CarClass?> LoadCarClassAsync(long carClassId)
        {
            return Task.Run<CarClass?>(() =>
            {
                using var connection = _database.OpenConnection();
                using var command = connection.CreateCommand();

                command.CommandText = SELECT_SINGLE_QUERY;
                command.Parameters.AddWithValue("@id", carClassId);

                using var reader = command.ExecuteReader();

                var mapper = CarClassMapper.Initialize(reader);
                if (reader.Read())
                {
                    return mapper.MapFromRow(reader);
                }
                return null;
            });
        }
    }
}
