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

    internal class TrackMapper : IDbRowMapper<Track>
    {
        private readonly TrackTableIndexes _indexes;

        private TrackMapper(TrackTableIndexes indexes)
        {
            _indexes = indexes;
        }

        public Track MapFromRow(DbDataReader reader)
        {
            var trackRaw = new TrackRaw(
                Id: reader.GetInt32(_indexes.Id),
                TypeId: reader.GetInt32(_indexes.TypeId),
                Name: reader.GetString(_indexes.Name),
                Length: reader.GetDouble(_indexes.Length)
            );

            return new Track(
                Raw: trackRaw,
                TypeName: reader.GetString(_indexes.TrackTypeName)
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
