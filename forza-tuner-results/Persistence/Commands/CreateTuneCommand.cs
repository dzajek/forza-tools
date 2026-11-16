using ForzaTunerResults.DataModel.Entities;
using ForzaTunerResults.Internal;
using System.Threading.Tasks;

namespace ForzaTunerResults.Persistence.Commands
{
    internal class CreateTuneCommand(DatabaseService database) : IDatabaseCommand<TuneEntity>
    {
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

        public Task Execute(TuneEntity tune)
        {
            return Task.Run(() =>
            {
                using var connection = database.OpenConnection();
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
    }
}
