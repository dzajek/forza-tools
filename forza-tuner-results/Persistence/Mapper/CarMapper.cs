using ForzaTunerResults.DataModel.DTOs;
using System.Data.Common;

namespace ForzaTunerResults.Persistence.Mapper
{
    internal sealed record CarTableIndexes(
        int Id,
        int Year,
        int Manufacturer,
        int Model

    );

    internal class CarMapper : IDatabaseRowMapper<Car>
    {
        private readonly CarTableIndexes _indexes;

        private CarMapper(CarTableIndexes indexes)
        {
            _indexes = indexes;
        }

        public Car MapFromRow(DbDataReader reader)
        {
            return new Car(
                 Id: reader.GetInt32(_indexes.Id),
                 Year: reader.GetInt32(_indexes.Year),
                 Manufacturer: reader.GetString(_indexes.Manufacturer),
                 Model: reader.GetString(_indexes.Model)
             );
        }

        public static CarMapper Initialize(DbDataReader reader)
        {
            var indexes = ReadTableIndexes(reader);
            return new CarMapper(indexes);
        }

        private static CarTableIndexes ReadTableIndexes(DbDataReader reader) => new(
            Id: reader.GetOrdinal("ID"),
            Year: reader.GetOrdinal("YEAR"),
            Manufacturer: reader.GetOrdinal("MANUFACTURER"),
            Model: reader.GetOrdinal("MODEL")
        );
    }
}
