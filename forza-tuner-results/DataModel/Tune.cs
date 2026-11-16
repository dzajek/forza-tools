using ForzaTunerResults.DataModel.DTOs;

namespace ForzaTunerResults.DataModel
{
    public sealed record TuneRaw(
        long Id,
        long CarId,
        long CarClassId,
        long TiresId,
        long SuspensionId,
        long DrivetrainId,
        long GearboxId,
        long FrontAeroId,
        long RearAeroId,
        string Engine,
        long Pi,
        long Weight,
        long Power,
        long Torque,
        double LateralGrip,
        bool IsDefault
    );

    public sealed record Tune(
        TuneRaw Raw,
        string CarClassPrefix,
        string Tires,
        string Suspension,
        string Drivetrain,
        string Gearbox,
        string FrontAero,
        string RearAero
        )
    {
        public string ClassDescription => CarClass.Describe(CarClassPrefix, Raw.Pi);
        public double PowerToWeight
        {
            get
            {
                if (Raw.Weight == 0)
                {
                    return 0;
                }
                return (double)Raw.Power / (double)Raw.Weight;
            }
        }
        public double TorqueToWeight
        {
            get
            {
                if (Raw.Weight == 0)
                {
                    return 0;
                }
                return (double)Raw.Torque / (double)Raw.Weight;
            }
        }
    }
}
