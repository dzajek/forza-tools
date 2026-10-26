using ForzaTunerResults.DataModel;
using ForzaTunerResults.Internal;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace ForzaTunerResults.Service
{
    public sealed class TrackTypeService(DatabaseService _database)
    {
        private const string SELECT_ALL_QUERY = """
            SELECT ID, NAME
            FROM TRACK_TYPE
            ORDER BY ID
        """;
        private const string INSERT_SINGLE_QUERY = """
            INSERT INTO
                TRACK_TYPE (NAME)
                VALUES (@name)
        """;

        public Task<Collection<TrackType>> LoadTrackTypesAsync()
        {
            return Task.Run<Collection<TrackType>>(() =>
            {
                using var connection = _database.OpenConnection();
                using var command = connection.CreateCommand();

                command.CommandText = SELECT_ALL_QUERY;

                using var reader = command.ExecuteReader();

                var trackTypes = new Collection<TrackType>();
                while (reader.Read())
                {
                    var id = reader.GetInt32(0);
                    var name = reader.GetString(1);
                    trackTypes.Add(new(id, name));
                }

                return trackTypes;
            });
        }

        public Task AddTrackTypeAsync(TrackType trackType)
        {
            return Task.Run(() =>
            {
                using var connection = _database.OpenConnection();
                using var command = connection.CreateCommand();

                command.CommandText = INSERT_SINGLE_QUERY;
                command.Parameters.AddWithValue("@name", trackType.Name);

                command.ExecuteReader();
            });
        }

    }
}
