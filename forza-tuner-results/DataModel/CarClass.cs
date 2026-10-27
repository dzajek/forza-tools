
namespace ForzaTunerResults.DataModel
{
    public record CarClass(long Id, string Letter, int PiLimit)
    {
        public string Name => $"{Letter} {PiLimit}";
    }
}
