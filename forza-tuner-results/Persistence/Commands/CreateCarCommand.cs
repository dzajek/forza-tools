using ForzaTunerResults.DataModel.Entities;
using ForzaTunerResults.Internal;
using System.Threading.Tasks;

namespace ForzaTunerResults.Persistence.Commands
{
    internal class CreateCarCommand(DatabaseService database) : IDatabaseCommand<CarEntity>
    {
        private const string INSERT_SINGLE_QUERY = """
            INSERT INTO
                CAR (YEAR, MANUFACTURER, MODEL)
                VALUES (@year, @manufacturer, @model)
        """;

        public Task Execute(CarEntity car)
        {
            return Task.Run(() =>
            {
                using var connection = database.OpenConnection();
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
