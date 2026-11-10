using System;

namespace ForzaTunerResults.DataModel
{
    public record NavigationItem(string Label, Type NavigationTarget, string? NavigationParam);

}
