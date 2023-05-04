using objective.Core;
using System.Collections;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace objective.Forms.UI.Units
{
		public class ToolGrid : ContentControl
		{
				public IEnumerable ToolfuncItemsSource
				{
						get { return (IEnumerable)GetValue (ToolfuncItemsSourceProperty); }
						set { SetValue (ToolfuncItemsSourceProperty, value); }
				}

				// Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
				public static readonly DependencyProperty ToolfuncItemsSourceProperty =
					DependencyProperty.Register ("ToolfuncItemsSource", typeof (IEnumerable), typeof (ToolGrid), new PropertyMetadata (null));

				public IEnumerable SelectItemSource
				{
						get { return (IEnumerable)GetValue (SelectItemSourceProperty); }
						set { SetValue (SelectItemSourceProperty, value); }
				}

				// Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
				public static readonly DependencyProperty SelectItemSourceProperty =
					DependencyProperty.Register ("SelectItemSource", typeof (IEnumerable), typeof (ToolGrid), new PropertyMetadata (null));


				static ToolGrid()
				{
						DefaultStyleKeyProperty.OverrideMetadata (typeof (ToolGrid), new FrameworkPropertyMetadata (typeof (ToolGrid)));
				}
		}
}
