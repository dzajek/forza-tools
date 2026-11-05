namespace ForzaTunerResults.ViewModel
{

    public enum DictionaryItemEnum
    {
        TIRES,
        SUSPENSION,
        DRIVETRAIN,
        GEARBOX,
        AERO,
        TRACKTYPE
    }

    public sealed record DictionaryItemType(DictionaryItemEnum Key, string TableName, string Label)
    {
        public static readonly DictionaryItemType Tires = new(DictionaryItemEnum.TIRES, "TIRES", "Tires");
        public static readonly DictionaryItemType Suspension = new(DictionaryItemEnum.SUSPENSION, "SUSPENSION", "Suspension");
        public static readonly DictionaryItemType Drivetrain = new(DictionaryItemEnum.DRIVETRAIN, "DRIVETRAIN", "Drivetrain");
        public static readonly DictionaryItemType Gearbox = new(DictionaryItemEnum.GEARBOX, "GEARBOX", "Gearbox");
        public static readonly DictionaryItemType Aero = new(DictionaryItemEnum.AERO, "AERO", "Aero");
        public static readonly DictionaryItemType TrackType = new(DictionaryItemEnum.TRACKTYPE, "TRACK_TYPE", "Track Type");
    }
}
