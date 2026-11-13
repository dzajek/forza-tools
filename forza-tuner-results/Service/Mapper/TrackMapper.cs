using ForzaTunerResults.DataModel;
using System.Data.Common;

namespace ForzaTunerResults.Service.Mapper
{
    internal sealed record TrackTableIndexes(
        int Id,
        int TypeId,
        int Name,
        int Length,
        int TrackTypeName
    );

    internal class TrackMapper : IDbRowMapper<TrackDescription>
    {
        private readonly TrackTableIndexes _indexes;

        private TrackMapper(TrackTableIndexes indexes)
        {
            _indexes = indexes;
        }

        public TrackDescription MapFromRow(DbDataReader reader)
        {
            return new TrackDescription(
                reader.GetInt32(_indexes.Id),
                reader.GetInt32(_indexes.TypeId),
                reader.GetString(_indexes.TrackTypeName),
                reader.GetString(_indexes.Name),
                reader.GetDouble(_indexes.Length)
            );
        }

        public static TrackMapper Initialize(DbDataReader reader)
        {
            var indexes = ReadTableIndexes(reader);
            return new TrackMapper(indexes);
        }

        private static TrackTableIndexes ReadTableIndexes(DbDataReader reader) => new(
            Id: reader.GetOrdinal("ID"),
            TypeId: reader.GetOrdinal("TYPE_ID"),
            Name: reader.GetOrdinal("NAME"),
            Length: reader.GetOrdinal("LENGTH"),
            TrackTypeName: reader.GetOrdinal("TRACK_TYPE_NAME")
        );
    }
}
