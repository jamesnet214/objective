using Jamesnet.Wpf.Controls;
using System.Windows;
using System.Windows.Input;

namespace objective.Forms.UI.Views
{
		public class MainContent : JamesContent
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
		}
}
