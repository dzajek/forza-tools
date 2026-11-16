using System.Data.Common;

namespace ForzaTunerResults.Persistence
{
    internal interface IDatabaseRowMapper<T>
    {
        public T MapFromRow(DbDataReader reader);
    }

}
