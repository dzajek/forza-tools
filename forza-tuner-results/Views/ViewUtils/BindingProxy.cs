using Microsoft.UI.Xaml;

namespace ForzaTunerResults.Views.ViewUtils
{
    public class BindingProxy : DependencyObject
    {
        public static readonly DependencyProperty DataProperty =
            DependencyProperty.Register("Data", typeof(object), typeof(BindingProxy), new PropertyMetadata(null));

        public object Data
        {
            get => GetValue(DataProperty);
            set => SetValue(DataProperty, value);
        }
    }
}