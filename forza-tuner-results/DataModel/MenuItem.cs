
namespace ForzaTunerResults.DataModel
{
    public sealed class MenuItem(string tag, string name, string icon)
    {
        private readonly string _tag = tag;
        private readonly string _name = name;
        private readonly string _icon = icon;

        public string Tag { get { return _tag; } }
        public string Name { get { return _name; } }
        public string Icon { get { return _icon; } }
    }
}
