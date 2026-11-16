using ForzaTunerResults.DataModel.Entities;
using ForzaTunerResults.Internal;
using System.Threading.Tasks;

namespace ForzaTunerResults.Persistence.Commands
{
    internal sealed class CreateCarClassCommand(
        DatabaseService databaseService
    ) : IDatabaseCommand<CarClassEntity>
    {
        private const string INSERT_SINGLE_QUERY = """
            INSERT INTO
                CAR_CLASS (PREFIX, PI_LIMIT)
                VALUES (@prefix, @piLimit)
        """;

        public Task Execute(CarClassEntity carClass)
        {
            return Task.Run(() =>
            {
                using var connection = databaseService.OpenConnection();
                using var command = connection.CreateCommand();

                command.CommandText = INSERT_SINGLE_QUERY;

                command.Parameters.AddWithValue("@prefix", carClass.Prefix);
                command.Parameters.AddWithValue("@piLimit", carClass.PiLimit);

                command.ExecuteNonQuery();
            });
        }
    }
}
