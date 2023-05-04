using System.Windows;
using System.Windows.Controls;

namespace objective.Forms.UI.Units
{
		public class ToolGrid : ContentControl
		{
				static ToolGrid()
				{
						DefaultStyleKeyProperty.OverrideMetadata (typeof (ToolGrid), new FrameworkPropertyMetadata (typeof (ToolGrid)));
				}
		}
}
