using System.Windows;
using System.Windows.Controls;

namespace objective.Forms.UI.Units
{
    public class ItemsListItem : ListBoxItem
    {
        static ItemsListItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ItemsListItem), new FrameworkPropertyMetadata(typeof(ItemsListItem)));
        }
    }
}
