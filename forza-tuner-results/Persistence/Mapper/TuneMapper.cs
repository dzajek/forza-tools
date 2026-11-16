using ForzaTunerResults.DataModel.DTOs;
using Microsoft.Data.Sqlite;
using System.Data.Common;

namespace ForzaTunerResults.Persistence.Mapper
{
    internal sealed record TuneTableIndexes(
        int Id,
        int CarId,
        int CarClassId,
        int TiresId,
        int SuspensionId,
        int DrivetrainId,
        int GearboxId,
        int FrontAeroId,
        int RearAeroId,
        int Engine,
        int Pi,
        int Weight,
        int Power,
        int Torque,
        int LateralGrip,
        int IsDefault,
        int CarClassPrefix,
        int Tires,
        int Suspension,
        int Drivetrain,
        int Gearbox,
        int FrontAero,
        int RearAero
    );

    internal sealed class TuneMapper : IDatabaseRowMapper<Tune>
    {
        private readonly TuneTableIndexes _indexes;

        private TuneMapper(TuneTableIndexes indexes)
        {
            _indexes = indexes;
        }

        public Tune MapFromRow(DbDataReader reader)
        {
            return new Tune(
                Id: reader.GetInt32(_indexes.Id),
                CarId: reader.GetInt32(_indexes.CarId),
                CarClassId: reader.GetInt32(_indexes.CarClassId),
                TiresId: reader.GetInt32(_indexes.TiresId),
                SuspensionId: reader.GetInt32(_indexes.SuspensionId),
                DrivetrainId: reader.GetInt32(_indexes.DrivetrainId),
                GearboxId: reader.GetInt32(_indexes.GearboxId),
                FrontAeroId: reader.GetInt32(_indexes.FrontAeroId),
                RearAeroId: reader.GetInt32(_indexes.RearAeroId),
                Engine: reader.GetString(_indexes.Engine),
                Pi: reader.GetInt32(_indexes.Pi),
                Weight: reader.GetInt32(_indexes.Weight),
                Power: reader.GetInt32(_indexes.Power),
                Torque: reader.GetInt32(_indexes.Torque),
                LateralGrip: reader.GetDouble(_indexes.LateralGrip),
                IsDefault: reader.GetBoolean(_indexes.IsDefault),
                CarClassPrefix: reader.GetString(_indexes.CarClassPrefix),
                Tires: reader.GetString(_indexes.Tires),
                Suspension: reader.GetString(_indexes.Suspension),
                Drivetrain: reader.GetString(_indexes.Drivetrain),
                Gearbox: reader.GetString(_indexes.Gearbox),
                FrontAero: reader.GetString(_indexes.FrontAero),
                RearAero: reader.GetString(_indexes.RearAero)
            );
        }

        public static TuneMapper Initialize(SqliteDataReader reader)
        {
            var indexes = ReadTableIndexes(reader);
            return new TuneMapper(indexes);
        }

        private static TuneTableIndexes ReadTableIndexes(SqliteDataReader reader) => new(
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
