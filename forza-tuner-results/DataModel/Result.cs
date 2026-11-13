using System;

namespace ForzaTunerResults.DataModel
{
    public sealed record ResultRaw(
        long Id,
        long TuneId,
        long TrackId,
        TimeSpan TimeTaken
    );

    public sealed record Result(
        ResultRaw Raw,
        string TrackName,
        double TrackLength,
        int CarYear,
        string CarManufacturer,
        string CarModel
    )
    {
        public double AverageSpeed
        {
            get
            {
                return (60 * 60 * 1000 * TrackLength) / Raw.TimeTaken.TotalMilliseconds;
            }
        }
    }
}
