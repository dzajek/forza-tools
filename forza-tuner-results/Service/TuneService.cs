using ForzaTunerResults.DataModel;
using ForzaTunerResults.Internal;
using ForzaTunerResults.Service.Mapper;
using Microsoft.Data.Sqlite;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace ForzaTunerResults.Service
{
    public sealed class TuneService(DatabaseService _database)
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
        private const string INSERT_SINGLE_QUERY = """
            INSERT INTO TUNE(
                CAR_ID,
                CAR_CLASS_ID,
                TIRES_ID,
                SUSPENSION_ID,
                DRIVETRAIN_ID,
                GEARBOX_ID,
                FRONT_AERO_ID,
                REAR_AERO_ID,
                ENGINE,
                PI,
                WEIGHT,
                POWER,
                TORQUE,
                LATERAL_GRIP,
                IS_DEFAULT)
            VALUES(
                @carId,
                @carClassId,
                @tiresId,
                @suspensionId,
                @drivetrainId,
                @gearboxId,
                @frontAeroId,
                @rearAeroId,
                @engine,
                @pi,
                @weight,
                @power,
                @torque,
                @lateralGrip,
                @isDefault
            )
            """;

        public Task<Collection<Tune>> LoadTunesAsync(long carId)
        {
            return Task.Run<Collection<Tune>>(() =>
            {
                using var connection = _database.OpenConnection();
                using var command = connection.CreateCommand();

                command.CommandText = SELECT_ALL_QUERY;
                command.Parameters.AddWithValue("@carId", carId);

                using var reader = command.ExecuteReader();

                var indexes = ReadTableIndexes(reader);
                var mapper = new TuneMapper(indexes);

                var tunes = new Collection<Tune>();
                while (reader.Read())
                {
                    tunes.Add(mapper.MapTune(reader));
                }
                return tunes;
            });
        }

        public Task AddTuneAsync(TuneRaw tune)
        {
            return Task.Run(() =>
            {
                using var connection = _database.OpenConnection();
                using var command = connection.CreateCommand();

                command.CommandText = INSERT_SINGLE_QUERY;
                command.Parameters.AddWithValue("@carId", tune.CarId);
                command.Parameters.AddWithValue("@carClassId", tune.CarClassId);
                command.Parameters.AddWithValue("@tiresId", tune.TiresId);
                command.Parameters.AddWithValue("@suspensionId", tune.SuspensionId);
                command.Parameters.AddWithValue("@drivetrainId", tune.DrivetrainId);
                command.Parameters.AddWithValue("@gearboxId", tune.GearboxId);
                command.Parameters.AddWithValue("@frontAeroId", tune.FrontAeroId);
                command.Parameters.AddWithValue("@rearAeroId", tune.RearAeroId);
                command.Parameters.AddWithValue("@engine", tune.Engine);
                command.Parameters.AddWithValue("@pi", tune.Pi);
                command.Parameters.AddWithValue("@weight", tune.Weight);
                command.Parameters.AddWithValue("@power", tune.Power);
                command.Parameters.AddWithValue("@torque", tune.Torque);
                command.Parameters.AddWithValue("@lateralGrip", tune.LateralGrip);
                command.Parameters.AddWithValue("@isDefault", tune.IsDefault);

                command.ExecuteNonQuery();
            });
        }

        private static TableIndexes ReadTableIndexes(SqliteDataReader reader) => new(
                Id: reader.GetOrdinal("ID"),
                CarId: reader.GetOrdinal("CAR_ID"),
                CarClassId: reader.GetOrdinal("CAR_CLASS_ID"),
                TiresId: reader.GetOrdinal("TIRES_ID"),
                SuspensionId: reader.GetOrdinal("SUSPENSION_ID"),
                DrivetrainId: reader.GetOrdinal("DRIVETRAIN_ID"),
                GearboxId: reader.GetOrdinal("GEARBOX_ID"),
                FrontAeroId: reader.GetOrdinal("FRONT_AERO_ID"),
                RearAeroId: reader.GetOrdinal("REAR_AERO_ID"),
                Engine: reader.GetOrdinal("ENGINE"),
                Pi: reader.GetOrdinal("PI"),
                Weight: reader.GetOrdinal("WEIGHT"),
                Power: reader.GetOrdinal("POWER"),
                Torque: reader.GetOrdinal("TORQUE"),
                LateralGrip: reader.GetOrdinal("LATERAL_GRIP"),
                IsDefault: reader.GetOrdinal("IS_DEFAULT"),
                CarClassPrefix: reader.GetOrdinal("CAR_CLASS_PREFIX"),
                Tires: reader.GetOrdinal("TIRES_NAME"),
                Suspension: reader.GetOrdinal("SUSPENSION_NAME"),
                Drivetrain: reader.GetOrdinal("DRIVETRAIN_NAME"),
                Gearbox: reader.GetOrdinal("GEARBOX_NAME"),
                FrontAero: reader.GetOrdinal("FRONT_AERO_NAME"),
                RearAero: reader.GetOrdinal("REAR_AERO_NAME")
            );
    }
}
