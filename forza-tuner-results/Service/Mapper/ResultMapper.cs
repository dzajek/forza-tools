using ForzaTunerResults.DataModel;
using Microsoft.Data.Sqlite;
using System;
using System.Data.Common;

namespace ForzaTunerResults.Service.Mapper
{
    internal sealed record ResultTableIndexes(
        int Id,
        int TuneId,
        int TrackId,
        int TimeHours,
        int TimeMinutes,
        int TimeSeconds,
        int TimeFraction,
        int TrackName,
        int TrackLength,
        int CarYear,
        int CarManufacturer,
        int CarModel
    );

    internal sealed class ResultMapper : IDbRowMapper<Result>
    {
        private readonly ResultTableIndexes _indexes;

        private ResultMapper(ResultTableIndexes indexes)
        {
            _indexes = indexes;
        }

        public Result MapFromRow(DbDataReader reader)
        {
            var timeTaken = new TimeSpan(
                0,
                reader.GetInt32(_indexes.TimeHours),
                reader.GetInt32(_indexes.TimeMinutes),
                reader.GetInt32(_indexes.TimeSeconds),
                reader.GetInt32(_indexes.TimeFraction)
            );
            return new Result(
                Raw: new ResultRaw(
                    Id: reader.GetInt32(_indexes.Id),
                    TuneId: reader.GetInt32(_indexes.TuneId),
                    TrackId: reader.GetInt32(_indexes.TrackId),
                    TimeTaken: timeTaken
                ),
                TrackName: reader.GetString(_indexes.TrackName),
                TrackLength: reader.GetDouble(_indexes.TrackLength),
                CarYear: reader.GetInt32(_indexes.CarYear),
                CarManufacturer: reader.GetString(_indexes.CarManufacturer),
                CarModel: reader.GetString(_indexes.CarModel)
            );
        }

        public static ResultMapper Initialize(SqliteDataReader reader)
        {
            var indexes = ReadTableIndexes(reader);
            return new ResultMapper(indexes);
        }

        private static ResultTableIndexes ReadTableIndexes(SqliteDataReader reader) => new(
                Id: reader.GetOrdinal("ID"),
                TuneId: reader.GetOrdinal("TUNE_ID"),
                TrackId: reader.GetOrdinal("TRACK_ID"),
                TimeHours: reader.GetOrdinal("TIME_HOURS"),
                TimeMinutes: reader.GetOrdinal("TIME_MINUTES"),
                TimeSeconds: reader.GetOrdinal("TIME_SECONDS"),
                TimeFraction: reader.GetOrdinal("TIME_FRACTION"),
                TrackName: reader.GetOrdinal("TRACK_NAME"),
                TrackLength: reader.GetOrdinal("TRACK_LENGTH"),
                CarYear: reader.GetOrdinal("CAR_YEAR"),
                CarManufacturer: reader.GetOrdinal("CAR_MANUFACTURER"),
                CarModel: reader.GetOrdinal("CAR_MODEL")
            );
    }
}
