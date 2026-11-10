
namespace ForzaTunerResults.DataModel
{
    public record CarClass(long Id, string Prefix, int PiLimit)
    {
        public string Name => CarClass.Describe(Prefix, PiLimit);

        public static string Describe(string prefix, long piLimit) => $"{prefix} {piLimit}";

    }
}
