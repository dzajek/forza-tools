
namespace ForzaTunerResults.DataModel
{
    public sealed class CarClass(long id, string letter, int piLimit)
    {
        private readonly long _id = id;
        public long Id { get { return _id; } }

        private readonly string _letter = letter;
        public string Letter { get { return _letter; } }

        public int PiLimit { get; } = piLimit;

        public string Name => $"{Letter} {PiLimit}";

    }
}
