using ForzaTunerResults.DataModel.DTOs;
using ForzaTunerResults.Internal;
using ForzaTunerResults.Persistence.Mapper;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace ForzaTunerResults.Persistence.Queries
{
    internal class GetAllCarsQuery(DatabaseService database) : IDatabaseQuery<Collection<Car>>
    {
        private const string SELECT_ALL_QUERY = """
            SELECT ID, YEAR, MANUFACTURER, MODEL
            FROM CAR
            ORDER BY YEAR, MANUFACTURER, MODEL
        """;

        public Task<Collection<Car>> Execute()
        {
            return Task.Run<Collection<Car>>(() =>
            {
                using var connection = database.OpenConnection();
                using var command = connection.CreateCommand();

                command.CommandText = SELECT_ALL_QUERY;

                using var reader = command.ExecuteReader();

                var mapper = CarMapper.Initialize(reader);
                var cars = new Collection<Car>();
                while (reader.Read())
                {
                    cars.Add(mapper.MapFromRow(reader));
                }
                return cars;
            });
        }
    }
}
