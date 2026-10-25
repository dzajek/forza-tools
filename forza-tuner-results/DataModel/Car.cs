
namespace ForzaTunerResults.DataModel
{
    public sealed class Car(long id, int year, string manufacturer, string name)
    {
        private readonly long _id = id;
        public long Id { get { return _id; } }

        private readonly int _year = year;
        public int Year { get { return _year; } }

        private readonly string _manufacturer = manufacturer;
        public string Manufacturer { get { return _manufacturer; } }

        private readonly string _name = name;
        public string Name { get { return _name; } }
    }
}
