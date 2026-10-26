
namespace ForzaTunerResults.DataModel
{
    public sealed class TrackType(long id, string name)
    {
        private readonly long _id = id;
        public long Id { get { return _id; } }

        private readonly string _name = name;
        public string Name { get { return _name; } }
    }
}
