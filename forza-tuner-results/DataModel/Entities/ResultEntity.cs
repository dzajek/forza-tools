using System;

namespace ForzaTunerResults.DataModel.Entities
{
    public sealed record ResultEntity(
        long Id,
        long TuneId,
        long TrackId,
        TimeSpan TimeTaken
    );
}
