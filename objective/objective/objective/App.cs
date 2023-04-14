using Jamesnet.Wpf.Controls;
using objective.Forms.UI.Views;
using System.Windows;

namespace objective
{
		public class App : JamesApplication
		{
				protected override Window CreateShell()
				{
						return new MainWindow ();
				}
		}
}
