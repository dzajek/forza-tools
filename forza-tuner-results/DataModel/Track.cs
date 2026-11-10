namespace ForzaTunerResults.DataModel
{
    public record Track(long Id, long TypeId, string Name, double Length);

    public record TrackDescription(long Id, string TypeName, string Name, double Length)
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
