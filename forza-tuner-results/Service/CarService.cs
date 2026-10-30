using ForzaTunerResults.DataModel;
using ForzaTunerResults.Internal;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace ForzaTunerResults.Service
{
    public sealed class CarService(DatabaseService _database)
    {

        private const string SELECT_ALL_QUERY = """
            SELECT ID, YEAR, MANUFACTURER, MODEL
            FROM CAR
            ORDER BY YEAR, MANUFACTURER, MODEL
        """;

        private const string INSERT_SINGLE_QUERY = """
            INSERT INTO
                CAR (YEAR, MANUFACTURER, MODEL)
                VALUES (@year, @manufacturer, @model)
        """;

        public Task<Collection<Car>> LoadCarsAsync()
        {
            return Task.Run<Collection<Car>>(() =>
            {
                using var connection = _database.OpenConnection();
                using var command = connection.CreateCommand();

                command.CommandText = SELECT_ALL_QUERY;

                using var reader = command.ExecuteReader();

                var cars = new Collection<Car>();
                while (reader.Read())
                {
                    var id = reader.GetInt32(0);
                    var year = reader.GetInt32(1);
                    var manufacturer = reader.GetString(2);
                    var model = reader.GetString(3);
                    cars.Add(new(id, year, manufacturer, model));
                }
                return cars;
            });
        }

        public Task AddCarAsync(Car car)
        {
            return Task.Run(() =>
            {
                using var connection = _database.OpenConnection();
                using var command = connection.CreateCommand();

                command.CommandText = INSERT_SINGLE_QUERY;
                command.Parameters.AddWithValue("@year", car.Year);
                command.Parameters.AddWithValue("@manufacturer", car.Manufacturer);
                command.Parameters.AddWithValue("@model", car.Model);

                command.ExecuteNonQuery();
            });
        }

    }
}
