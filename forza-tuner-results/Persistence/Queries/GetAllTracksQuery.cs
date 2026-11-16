using ForzaTunerResults.DataModel.DTOs;
using ForzaTunerResults.Internal;
using ForzaTunerResults.Persistence.Mapper;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace ForzaTunerResults.Persistence.Queries
{
    internal class GetAllTracksQuery(DatabaseService database) : IDatabaseQuery<Collection<Track>>
    {
        private const string SELECT_ALL_QUERY = """
            SELECT T.ID, T.TYPE_ID, T.NAME, T.LENGTH, TT.NAME as TRACK_TYPE_NAME
            FROM TRACK T
            JOIN TRACK_TYPE TT ON T.TYPE_ID = TT.ID
            ORDER BY T.ID
        """;

        public Task<Collection<Track>> Execute()
        {
            return Task.Run<Collection<Track>>(() =>
            {
                using var connection = database.OpenConnection();
                using var command = connection.CreateCommand();

                command.CommandText = SELECT_ALL_QUERY;

                using var reader = command.ExecuteReader();

                var mapper = TrackMapper.Initialize(reader);
                var tracks = new Collection<Track>();
                while (reader.Read())
                {
                    tracks.Add(mapper.MapFromRow(reader));
                }

                return tracks;
            });
        }
    }
}
