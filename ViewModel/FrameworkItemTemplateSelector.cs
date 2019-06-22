using System.Windows;
using System.Windows.Controls;

namespace VSNewProjectDialogExample.ViewModel
{
    public class FrameworkItemTemplateSelector : DataTemplateSelector
    {
        public DataTemplate FrameworkTargetTemplate { get; set; }
        public DataTemplate LinkFrameworkTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item is FrameworkItemLinkViewModel)
                return LinkFrameworkTemplate;

            return FrameworkTargetTemplate;
        }
    }
}