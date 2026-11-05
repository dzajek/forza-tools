
namespace ForzaTunerResults.DataModel
{
    public record CarClass(long Id, string Prefix, int PiLimit)
    {
        public string Name => $"{Prefix} {PiLimit}";
    }
}
