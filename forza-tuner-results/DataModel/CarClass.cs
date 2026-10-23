
namespace ForzaTunerResults.DataModel
{
    public sealed class CarClass(long id, string letter, int maxPI)
    {
        private readonly long _id = id;
        public long Id { get { return _id; } }

        private readonly string _letter = letter;
        public string Letter { get { return _letter; } }

        private readonly int _maxPI = maxPI;
        public int MaxPI { get { return _maxPI; } }

        public string Name => $"{Letter} {MaxPI}";

    }
}
