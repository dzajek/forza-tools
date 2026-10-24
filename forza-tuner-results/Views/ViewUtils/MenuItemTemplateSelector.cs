using ForzaTunerResults.DataModel;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;


namespace ForzaTunerResults.Views.ViewUtils
{
    internal class MenuItemTemplateSelector : DataTemplateSelector
    {
        
        public DataTemplate? CarClassMenuItemTemplate { get; set; }
        public DataTemplate? StringMenuItemTemplate { get; set; }

        protected override DataTemplate? SelectTemplateCore(object item)
        {
            if (item == null)
            {
                return null;
            }

            if (item is CarClass)
            {
                return CarClassMenuItemTemplate;
            }

            if (item is string)
            {
                return StringMenuItemTemplate;
            }

            return null;
        }

    }
}
