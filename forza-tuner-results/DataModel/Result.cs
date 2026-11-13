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
        int CarYear,
        string CarManufacturer,
        string CarModel
    );
}
