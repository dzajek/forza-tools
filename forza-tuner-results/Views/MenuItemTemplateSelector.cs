

using ForzaTunerResults.DataModel;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Markup;


namespace ForzaTunerResults.Views
{
    [ContentProperty(Name = "MenuItemTemplate")]
    internal class MenuItemTemplateSelector : DataTemplateSelector
    {
        
        public DataTemplate? MenuItemTemplate { get; set; }


        protected override DataTemplate? SelectTemplateCore(object item)
        {
            if (item == null)
            {
                return null;
            }

            if (item is MenuItem)
            {
                return MenuItemTemplate;
            }

            return null;
        }

    }
}
