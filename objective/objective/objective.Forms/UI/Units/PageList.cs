using System.Windows;
using System.Windows.Controls;

namespace objective.Forms.UI.Units
{
		public class PageList : ListBox
		{
				static PageList()
				{
						DefaultStyleKeyProperty.OverrideMetadata (typeof (PageList), new FrameworkPropertyMetadata (typeof (PageList)));
				}
				protected override DependencyObject GetContainerForItemOverride()
				{
						return new PageListItem ();
				}
		}
}
