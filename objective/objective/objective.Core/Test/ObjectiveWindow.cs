using Jamesnet.Wpf.Controls;
using System;
using System.Windows;

namespace objective.Core.Test
{
		public class ObjectiveWindow : JamesWindow
		{
				public ObjectiveWindow()
				{
						Closed += ObjectiveWindow_Closed;
				}

				private void ObjectiveWindow_Closed(object? sender, EventArgs e)
				{
						FrameworkElement frameworkElement = sender as FrameworkElement;
						IViewClosedable viewClosedable = default;
						viewClosedable = frameworkElement.DataContext as IViewClosedable;
						viewClosedable.OnCloesd ();
				}
		}
}
