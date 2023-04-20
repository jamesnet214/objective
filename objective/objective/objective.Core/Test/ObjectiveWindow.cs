using Jamesnet.Wpf.Controls;
using System;
using System.Windows;

namespace objective.Core.Test
{
		public class ObjectiveWindow : JamesWindow
		{
				public ObjectiveWindow()
				{
						Closing += ObjectiveWindow_Closing;
						;
				}

				private void ObjectiveWindow_Closing(object? sender, System.ComponentModel.CancelEventArgs e)
				{
						FrameworkElement frameworkElement = sender as FrameworkElement;
						IViewClosedable viewClosedable = default;
						viewClosedable = frameworkElement.DataContext as IViewClosedable;
						viewClosedable.OnCloesd ();
				}
		}
}
