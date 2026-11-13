using ForzaTunerResults.DataModel;
using System.Data.Common;

namespace ForzaTunerResults.Service.Mapper
{
    internal sealed record CarClassTableIndexes(
        int Id,
        int Prefix,
        int PiLimit
    );

    internal sealed class CarClassMapper : IDbRowMapper<CarClass>
    {
        private readonly CarClassTableIndexes _indexes;

        private CarClassMapper(CarClassTableIndexes indexes)
        {
            _indexes = indexes;
        }

        public CarClass MapFromRow(DbDataReader reader)
        {
            return new CarClass(
                Id: reader.GetInt32(_indexes.Id),
                Prefix: reader.GetString(_indexes.Prefix),
                PiLimit: reader.GetInt32(_indexes.PiLimit)
            );
        }

        public static CarClassMapper Initialize(DbDataReader reader)
        {
            var indexes = ReadTableIndexes(reader);
            return new CarClassMapper(indexes);
        }

        private static CarClassTableIndexes ReadTableIndexes(DbDataReader reader) => new(
            Id: reader.GetOrdinal("ID"),
            Prefix: reader.GetOrdinal("PREFIX"),
            PiLimit: reader.GetOrdinal("PI_LIMIT")
        );
    }
}
