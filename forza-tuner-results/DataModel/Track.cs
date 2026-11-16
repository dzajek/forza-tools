namespace ForzaTunerResults.DataModel
{
    public record TrackRaw(
        long Id,
        long TypeId,
        string Name,
        double Length
    );

    public record Track(
        TrackRaw Raw,
        string TypeName
    );
}
