namespace ForzaTunerResults.DataModel.Entities
{
    public record TrackEntity(
        long Id,
        long TypeId,
        string Name,
        double Length
    );
}
