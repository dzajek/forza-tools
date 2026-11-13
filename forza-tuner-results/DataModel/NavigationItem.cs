using System;

namespace ForzaTunerResults.DataModel
{
    public record NavigationParamSet(
        long TrackType,
        long TrackId,
        long ClassId
    );

    public record NavigationItem(
        string Label,
        Type NavigationTarget,
        NavigationParamSet? NavigationParam,
        NavigationItem[] Children
    );
}
