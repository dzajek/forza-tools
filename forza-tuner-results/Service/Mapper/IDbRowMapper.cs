using System.Data.Common;

namespace ForzaTunerResults.Service.Mapper
{
    internal interface IDbRowMapper<T>
    {
        public T MapFromRow(DbDataReader reader);
    }

}
