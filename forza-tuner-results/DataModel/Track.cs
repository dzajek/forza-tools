namespace ForzaTunerResults.DataModel
{
    public record Track(long Id, long TypeId, string Name, double Length);

    public record TrackDescription(long Id, long TypeId, string TypeName, string Name, double Length);
}
