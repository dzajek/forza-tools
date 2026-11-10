using ForzaTunerResults.DataModel;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace ForzaTunerResults.Views.Component;

public sealed partial class TuneListItem : UserControl
{
    public Tune Tune
    {
        get => (Tune)GetValue(TuneProperty);
        set => SetValue(TuneProperty, value);
    }

    public static readonly DependencyProperty TuneProperty =
            DependencyProperty.Register(
                nameof(Tune),
                typeof(Tune),
                typeof(TuneListItem),
                new PropertyMetadata(null));

    public TuneListItem()
    {
        InitializeComponent();
    }
}
