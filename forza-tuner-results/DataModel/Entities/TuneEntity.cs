namespace ForzaTunerResults.DataModel.Entities
{
    public sealed record TuneEntity(
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
}
