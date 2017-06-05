using Assistence_Control.Utilerias.Items;
using System.Globalization;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace Assistence_Control.TemplateSelectors
{
    public class MenuTemplateSelector : DataTemplateSelector
    {
        public DataTemplate MenuTabletTemplate { get; set; }
        public DataTemplate MenuMobileTemplate { get; set; }
        public DataTemplate MenuMobileTemplateLarge { get; set; }
        public DataTemplate MenuTabletTemplateLarge { get; set; }

        protected override DataTemplate SelectTemplateCore(object item, DependencyObject container)
        {
            DataTemplate template = null;
            DatosMenu menu = (DatosMenu)item;
            Frame rootFrame = (Window.Current.Content as Frame);
            if (rootFrame.ActualWidth <= 320)
            {
                if(menu.TileSize == 1)
                {
                    template = MenuMobileTemplate;
                }
                else
                {
                    template = MenuMobileTemplateLarge;
                }
            }
            if (rootFrame.ActualWidth >= 320 && rootFrame.ActualWidth < 720)
            {
                if (menu.TileSize == 1)
                {
                    template = MenuMobileTemplate; 
                }
                else
                {
                    template = MenuMobileTemplateLarge;
                }
            }
            if (rootFrame.ActualWidth >= 720)
            {
                if (menu.TileSize == 1)
                {
                    template = MenuTabletTemplate;

                }
                else
                {
                    template = MenuTabletTemplateLarge;
                }
            }
            paintMenuCustomColor(menu, (GridViewItem)container);
            return template;
        }

        private void paintMenuCustomColor(DatosMenu menu,GridViewItem itemContainer)
        {
            SolidColorBrush colorBackground = new SolidColorBrush();
            Color color = new Color();
            string hexCode = menu.BackGroundColor;
            color.A = 255;
            color.R = byte.Parse(hexCode.Substring(1, 2), NumberStyles.AllowHexSpecifier);
            color.G = byte.Parse(hexCode.Substring(3, 2), NumberStyles.AllowHexSpecifier);
            color.B = byte.Parse(hexCode.Substring(5, 2), NumberStyles.AllowHexSpecifier);
            colorBackground.Color = color;
            itemContainer.Background = colorBackground;
        }
    }
}