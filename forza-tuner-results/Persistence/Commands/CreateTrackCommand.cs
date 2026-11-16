using ForzaTunerResults.DataModel.Entities;
using ForzaTunerResults.Internal;
using System.Threading.Tasks;

namespace ForzaTunerResults.Persistence.Commands
{
    internal class CreateTrackCommand(DatabaseService database) : IDatabaseCommand<TrackEntity>
    {
        private const string INSERT_SINGLE_QUERY = """
            INSERT INTO
                TRACK (TYPE_ID, NAME, LENGTH)
                VALUES (@typeId, @name, @length)
        """;

        public Task Execute(TrackEntity track)
        {
            return Task.Run(() =>
            {
                using var connection = database.OpenConnection();
                using var command = connection.CreateCommand();

                command.CommandText = INSERT_SINGLE_QUERY;

                command.Parameters.AddWithValue("@typeId", track.TypeId);
                command.Parameters.AddWithValue("@name", track.Name);
                command.Parameters.AddWithValue("@length", track.Length);

                command.ExecuteNonQuery();
            });
        }
    }
}
