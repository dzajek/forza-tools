using ForzaTunerResults.DataModel;
using ForzaTunerResults.Internal;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace ForzaTunerResults.Service
{
    public sealed class TrackService(DatabaseService _database)
    {
        private const string SELECT_ALL_QUERY = """
            SELECT T.ID, T.TYPE_ID, T.NAME, T.LENGTH, TT.NAME as TRACK_TYPE_NAME
            FROM TRACK T
            JOIN TRACK_TYPE TT ON T.TYPE_ID = TT.ID
            ORDER BY T.ID
        """;
        private const string INSERT_SINGLE_QUERY = """
            INSERT INTO
                TRACK (TYPE_ID, NAME, LENGTH)
                VALUES (@typeId, @name, @length)
        """;

        public Task<Collection<TrackDescription>> LoadAllTracks()
        {
            return Task.Run<Collection<TrackDescription>>(() =>
            {
                using var connection = _database.OpenConnection();
                using var command = connection.CreateCommand();

                command.CommandText = SELECT_ALL_QUERY;

                using var reader = command.ExecuteReader();

                var tracks = new Collection<TrackDescription>();
                while (reader.Read())
                {
                    var id = reader.GetInt32(0);
                    var name = reader.GetString(2);
                    var length = reader.GetFloat(3);
                    var typeName = reader.GetString(4);
                    tracks.Add(new(id, typeName, name, length));
                }

                return tracks;
            });
        }

        public Task AddTrackAsync(Track track)
        {
            return Task.Run(() =>
            {
                using var connection = _database.OpenConnection();
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
