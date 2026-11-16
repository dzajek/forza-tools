using ForzaTunerResults.DataModel.DTOs;
using ForzaTunerResults.Internal;
using ForzaTunerResults.Internal.Exception;
using ForzaTunerResults.Persistence.Mapper;
using System.Threading.Tasks;

namespace ForzaTunerResults.Persistence.Queries
{
    internal class GetSingleCarClassQuery(DatabaseService database, long carClassId) : IDatabaseQuery<CarClass>
    {
        private const string SELECT_SINGLE_QUERY = """
            SELECT ID, PREFIX, PI_LIMIT
            FROM CAR_CLASS
            WHERE ID = @id
        """;

        public Task<CarClass> Execute()
        {
            return Task.Run<CarClass>(() =>
            {
                using var connection = database.OpenConnection();
                using var command = connection.CreateCommand();

                command.CommandText = SELECT_SINGLE_QUERY;
                command.Parameters.AddWithValue("@id", carClassId);

                using var reader = command.ExecuteReader();

                var mapper = CarClassMapper.Initialize(reader);
                if (reader.Read())
                {
                    return mapper.MapFromRow(reader);
                }
                throw new EntityNotFoundException("CarClass", carClassId);
            });
        }
    }
}
