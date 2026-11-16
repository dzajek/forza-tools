namespace ForzaTunerResults.DataModel.DTOs
{

    public record Track(
        long Id,
        long TypeId,
        string Name,
        double Length,
        string TypeName
    );
}
