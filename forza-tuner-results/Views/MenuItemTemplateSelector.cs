

using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Markup;


namespace ForzaTunerResults.Views
{
    [ContentProperty(Name = "CarClassItemTemplate")]
    internal class MenuItemTemplateSelector : DataTemplateSelector
    {
        
        public DataTemplate? CarClassItemTemplate { get; set; }

        protected override DataTemplate? SelectTemplateCore(object item)
        {
            return CarClassItemTemplate;
        }

    }
}
