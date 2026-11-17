using ForzaTunerResults.DataModel.DTOs;
using ForzaTunerResults.DataModel.Exception;
using ForzaTunerResults.Internal;
using ForzaTunerResults.Persistence.Mapper;
using System.Threading.Tasks;

namespace ForzaTunerResults.Persistence.Queries
{
    internal class GetSingleTrackQuery(DatabaseService database, long trackTypeId, long trackId) : IDatabaseQuery<Track>
    {
        private const string SELECT_SINGLE_QUERY = """
            SELECT T.ID, T.TYPE_ID, T.NAME, T.LENGTH, TT.NAME as TRACK_TYPE_NAME
            FROM TRACK T
            JOIN TRACK_TYPE TT ON T.TYPE_ID = TT.ID
            WHERE T.TYPE_ID = @typeId AND T.ID = @trackId
        """;

        public Task<Track> Execute()
        {
            return Task.Run<Track>(() =>
            {
                using var connection = database.OpenConnection();
                using var command = connection.CreateCommand();

                command.CommandText = SELECT_SINGLE_QUERY;
                command.Parameters.AddWithValue("@typeId", trackTypeId);
                command.Parameters.AddWithValue("@trackId", trackId);

                using var reader = command.ExecuteReader();

                var mapper = TrackMapper.Initialize(reader);
                if (reader.Read())
                {
                    return mapper.MapFromRow(reader);
                }
                throw new EntityNotFoundException("Track", trackId);
            });
        }
    }
}
