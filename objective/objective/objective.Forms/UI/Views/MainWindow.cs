using Jamesnet.Wpf.Controls;
using System.Windows;

namespace objective.Forms.UI.Views
{
		public class MainWindow : JamesWindow
		{
				static MainWindow()
				{
						DefaultStyleKeyProperty.OverrideMetadata (typeof (MainWindow), new FrameworkPropertyMetadata (typeof (MainWindow)));
				}
		}
}
