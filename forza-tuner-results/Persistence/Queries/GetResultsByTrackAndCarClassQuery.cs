using ForzaTunerResults.DataModel.DTOs;
using ForzaTunerResults.Internal;
using ForzaTunerResults.Persistence.Mapper;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace ForzaTunerResults.Persistence.Queries
{
    internal class GetResultsByTrackAndCarClassQuery(
        DatabaseService database,
        long trackTypeId,
        long trackId,
        long carClassId
    ) : IDatabaseQuery<Collection<Result>>
    {
        private const string SELECT_ALL_QUERY = """
            SELECT
                RE.ID,
                RE.TUNE_ID,
                RE.TRACK_ID,
                RE.TIME_HOURS,
                RE.TIME_MINUTES,
                RE.TIME_SECONDS,
                RE.TIME_FRACTION,
                TR.NAME AS TRACK_NAME,
                TR.LENGTH AS TRACK_LENGTH,
                CA.YEAR AS CAR_YEAR,
                CA.MANUFACTURER AS CAR_MANUFACTURER,
                CA.MODEL AS CAR_MODEL
            FROM RESULT RE
                JOIN TUNE TU ON RE.TUNE_ID = TU.ID
                JOIN TRACK TR ON RE.TRACK_ID = TR.ID
                JOIN CAR CA ON TU.CAR_ID = CA.ID
            WHERE
                TR.TYPE_ID = @trackTypeId AND
                TR.ID = @trackId AND
                TU.CAR_CLASS_ID = @carClassId
            ORDER BY
                RE.TIME_HOURS DESC,
                RE.TIME_MINUTES DESC,
                RE.TIME_SECONDS DESC,
                RE.TIME_FRACTION DESC
        """;

        public Task<Collection<Result>> Execute()
        {
            return Task.Run<Collection<Result>>(() =>
            {
                using var connection = database.OpenConnection();
                using var command = connection.CreateCommand();

                command.CommandText = SELECT_ALL_QUERY;
                command.Parameters.AddWithValue("trackTypeId", trackTypeId);
                command.Parameters.AddWithValue("trackId", trackId);
                command.Parameters.AddWithValue("carClassId", carClassId);

                using var reader = command.ExecuteReader();

                var mapper = ResultMapper.Initialize(reader);
                var results = new Collection<Result>();
                while (reader.Read())
                {
                    results.Add(mapper.MapFromRow(reader));
                }

                return results;
            });
        }
    }
}
