using System;

namespace ForzaTunerResults.DataModel.DTOs
{

    public sealed record Result(
        long Id,
        long TuneId,
        long TrackId,
        TimeSpan TimeTaken,
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
                return (60 * 60 * 1000 * TrackLength) / TimeTaken.TotalMilliseconds;
            }
        }
    }
}
