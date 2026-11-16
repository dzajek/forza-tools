namespace ForzaTunerResults.DataModel.DTOs
{
    public sealed record Tune(
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
        bool IsDefault,
        string CarClassPrefix,
        string Tires,
        string Suspension,
        string Drivetrain,
        string Gearbox,
        string FrontAero,
        string RearAero
        )
    {
        public string ClassDescription => CarClass.Describe(CarClassPrefix, Pi);
        public double PowerToWeight
        {
            get
            {
                if (Weight == 0)
                {
                    return 0;
                }
                return (double)Power / (double)Weight;
            }
        }
        public double TorqueToWeight
        {
            get
            {
                if (Weight == 0)
                {
                    return 0;
                }
                return (double)Torque / (double)Weight;
            }
        }
    }
}
