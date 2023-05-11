using Jamesnet.Wpf.Controls;
using System;
using System.Windows;
using System.Windows.Input;

namespace objective.Forms.UI.Views
{
		public interface IViewAccess
		{
				FrameworkElement GetData();
		}

		public class MainContent : JamesContent, IViewAccess
        {
				public static readonly DependencyProperty MultiSelectItemCommandProperty = DependencyProperty.Register ("MultiSelectItemCommand", typeof (ICommand), typeof (MainContent), new PropertyMetadata (null));

				public ICommand MultiSelectItemCommand
				{
						get { return (ICommand)GetValue (MultiSelectItemCommandProperty); }
						set { SetValue (MultiSelectItemCommandProperty, value); }
				}

				static MainContent()
                {
                        DefaultStyleKeyProperty.OverrideMetadata(typeof(MainContent), new FrameworkPropertyMetadata(typeof(MainContent)));
                }
                public MainContent()
                {
						KeyDown += MainContent_PreviewKeyDown;
						PreviewKeyUp += MainContent_PreviewKeyUp;
                }

				protected override void OnInitialized(EventArgs e)
				{
						base.OnInitialized (e);
				}

				private void MainContent_PreviewKeyUp(object sender, KeyEventArgs e)
				{
						if (e.Key == Key.LeftCtrl)
						{
								MultiSelectItemCommand?.Execute(false);
						}
				}


				private void MainContent_PreviewKeyDown(object sender, KeyEventArgs e)
				{
						if (e.Key == Key.LeftCtrl)
						{
								MultiSelectItemCommand?.Execute(true);
						}
				}

				public FrameworkElement GetData()
				{
						return GetTemplateChild ("PART_PAGE") as FrameworkElement;
				}
		}
}
