namespace ForzaTunerResults.DataModel
{
    public record Track(long Id, long TypeId, string Name, float Length);

    public record TrackDescription(long Id, string TypeName, string Name, float Length)
    {
        public string FormattedLength
        {
            get
            {
                return Length.ToString("####.0km");
            }
        }
    }
}
