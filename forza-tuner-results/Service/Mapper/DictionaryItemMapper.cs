using ForzaTunerResults.DataModel.DTOs;
using System.Data.Common;

namespace ForzaTunerResults.Service.Mapper
{
    internal sealed record DictionaryItemTableIndexes(
        int Id,
        int Name
    );

    internal class DictionaryItemMapper : IDbRowMapper<DictionaryItem>
    {
        private readonly DictionaryItemTableIndexes _indexes;

        private DictionaryItemMapper(DictionaryItemTableIndexes indexes)
        {
            _indexes = indexes;
        }

        public DictionaryItem MapFromRow(DbDataReader reader)
        {
            var id = reader.GetInt32(_indexes.Id);
            var name = reader.GetString(_indexes.Name);
            return new DictionaryItem(id, name);
        }

        public static DictionaryItemMapper Initialize(DbDataReader reader)
        {
            var indexes = ReadTableIndexes(reader);
            return new DictionaryItemMapper(indexes);
        }

        private static DictionaryItemTableIndexes ReadTableIndexes(DbDataReader reader) => new(
            Id: reader.GetOrdinal("ID"),
            Name: reader.GetOrdinal("NAME")
        );
    }
}
