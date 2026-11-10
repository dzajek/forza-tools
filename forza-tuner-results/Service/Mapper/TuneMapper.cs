using ForzaTunerResults.DataModel;
using System.Data.Common;

namespace ForzaTunerResults.Service.Mapper
{
    internal sealed record TableIndexes(
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

    internal sealed class TuneMapper(TableIndexes indexes)
    {
        internal Tune MapTune(DbDataReader reader)
        {
            var raw = new TuneRaw(
                        Id: reader.GetInt32(indexes.Id),
                        CarId: reader.GetInt32(indexes.CarId),
                        CarClassId: reader.GetInt32(indexes.CarClassId),
                        TiresId: reader.GetInt32(indexes.TiresId),
                        SuspensionId: reader.GetInt32(indexes.SuspensionId),
                        DrivetrainId: reader.GetInt32(indexes.DrivetrainId),
                        GearboxId: reader.GetInt32(indexes.GearboxId),
                        FrontAeroId: reader.GetInt32(indexes.FrontAeroId),
                        RearAeroId: reader.GetInt32(indexes.RearAeroId),
                        Engine: reader.GetString(indexes.Engine),
                        Pi: reader.GetInt32(indexes.Pi),
                        Weight: reader.GetInt32(indexes.Weight),
                        Power: reader.GetInt32(indexes.Power),
                        Torque: reader.GetInt32(indexes.Torque),
                        LateralGrip: reader.GetFloat(indexes.LateralGrip),
                        IsDefault: reader.GetBoolean(indexes.IsDefault)
                    );
            return new Tune(
                Raw: raw,
                CarClassPrefix: reader.GetString(indexes.CarClassPrefix),
                Tires: reader.GetString(indexes.Tires),
                Suspension: reader.GetString(indexes.Suspension),
                Drivetrain: reader.GetString(indexes.Drivetrain),
                Gearbox: reader.GetString(indexes.Gearbox),
                FrontAero: reader.GetString(indexes.FrontAero),
                RearAero: reader.GetString(indexes.RearAero)
            );
        }

    }
}
