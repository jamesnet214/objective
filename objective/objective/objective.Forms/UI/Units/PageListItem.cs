using System.Windows;
using System.Windows.Controls;

namespace objective.Forms.UI.Units
{
		public class PageListItem : ListBoxItem
		{
				static PageListItem()
				{
						DefaultStyleKeyProperty.OverrideMetadata (typeof (PageListItem), new FrameworkPropertyMetadata (typeof (PageListItem)));
				}
		}
}
