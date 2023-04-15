using System.Windows;
using System.Windows.Controls;

namespace objective.Forms.UI.Units
{
        public class TableItem : ListBoxItem
        {
                static TableItem()
                {
                        DefaultStyleKeyProperty.OverrideMetadata(typeof(TableItem), new FrameworkPropertyMetadata(typeof(TableItem)));
                }
        }
}
