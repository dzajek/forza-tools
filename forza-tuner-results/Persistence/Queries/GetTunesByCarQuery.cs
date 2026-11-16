using ForzaTunerResults.DataModel.DTOs;
using ForzaTunerResults.Internal;
using ForzaTunerResults.Persistence.Mapper;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace ForzaTunerResults.Persistence.Queries
{
    internal class GetTunesByCarQuery(DatabaseService database, long carId) : IDatabaseQuery<Collection<Tune>>
    {
        private const string SELECT_ALL_QUERY = """
            SELECT
                TU.ID,
                TU.CAR_ID,
                TU.CAR_CLASS_ID,
                TU.TIRES_ID,
                TU.SUSPENSION_ID,
                TU.DRIVETRAIN_ID,
                TU.GEARBOX_ID,
                TU.FRONT_AERO_ID,
                TU.REAR_AERO_ID,
                TU.ENGINE,
                TU.PI,
                TU.WEIGHT,
                TU.POWER,
                TU.TORQUE,
                TU.LATERAL_GRIP,
                TU.IS_DEFAULT,
                CC.PREFIX AS CAR_CLASS_PREFIX,
                TI.NAME AS TIRES_NAME,
                SU.NAME AS SUSPENSION_NAME,
                DR.NAME AS DRIVETRAIN_NAME,
                GB.NAME AS GEARBOX_NAME,
                FA.NAME AS FRONT_AERO_NAME,
                RA.NAME AS REAR_AERO_NAME
            FROM TUNE TU
                JOIN CAR_CLASS CC ON TU.CAR_CLASS_ID = CC.ID
                JOIN TIRES TI ON TU.TIRES_ID = TI.ID
                JOIN SUSPENSION SU ON TU.SUSPENSION_ID = SU.ID
                JOIN DRIVETRAIN DR ON TU.DRIVETRAIN_ID = DR.ID
                JOIN GEARBOX GB ON TU.GEARBOX_ID = GB.ID
                JOIN AERO FA ON TU.FRONT_AERO_ID = FA.ID
                JOIN AERO RA ON TU.REAR_AERO_ID = RA.ID
            WHERE TU.CAR_ID  = @carId
            ORDER BY TU.PI ASC
        """;

        public Task<Collection<Tune>> Execute()
        {
            return Task.Run<Collection<Tune>>(() =>
            {
                using var connection = database.OpenConnection();
                using var command = connection.CreateCommand();

                command.CommandText = SELECT_ALL_QUERY;
                command.Parameters.AddWithValue("@carId", carId);

                using var reader = command.ExecuteReader();

                var mapper = TuneMapper.Initialize(reader);

                var tunes = new Collection<Tune>();
                while (reader.Read())
                {
                    tunes.Add(mapper.MapFromRow(reader));
                }
                return tunes;
            });
        }
    }
}
