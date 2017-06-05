using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Assistence_Control.TemplateSelectors
{
    public class MenuCheckListTemplateSelector : DataTemplateSelector
    {
        public DataTemplate MenuTabletCheckListTemplate { get; set; }
        public DataTemplate MenuMobileCheckListTemplate { get; set; }
        protected override DataTemplate SelectTemplateCore(object item, DependencyObject container)
        {
            DataTemplate template = null;
            Frame rootFrame = (Window.Current.Content as Frame);
            if (rootFrame.ActualWidth <= 320)
                template = MenuMobileCheckListTemplate;
            if (rootFrame.ActualWidth >= 320 && rootFrame.ActualWidth < 720)
                template = MenuMobileCheckListTemplate;
            if (rootFrame.ActualWidth >= 720)
                template = MenuTabletCheckListTemplate;
            return template;                     
        }
    }
}
