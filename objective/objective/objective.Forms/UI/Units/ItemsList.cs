using objective.Forms.Local.Models;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace objective.Forms.UI.Units
{
		public class ItemsList : ListBox
		{
				public ICommand NewPageCommand
				{
						get { return (ICommand)GetValue (NewPageCommandProperty); }
						set { SetValue (NewPageCommandProperty, value); }
				}

				// Using a DependencyProperty as the backing store for NewPageCommand.  This enables animation, styling, binding, etc...
				public static readonly DependencyProperty NewPageCommandProperty =
					DependencyProperty.Register ("NewPageCommand", typeof (ICommand), typeof (ItemsList), new PropertyMetadata (null));


				static ItemsList()
				{
						DefaultStyleKeyProperty.OverrideMetadata (typeof (ItemsList), new FrameworkPropertyMetadata (typeof (ItemsList)));
				}

				protected override DependencyObject GetContainerForItemOverride()
				{
						return new ItemsListItem ();
				}

				private object dragData;

				public ItemsList()
				{
						DragEnter += ListBox_DragEnter;
						DragLeave += ListBox_DragLeave;
						DragOver += ListBox_DragOver;

						PreviewMouseLeftButtonDown += ItemsList_PreviewMouseLeftButtonDown;
				}

				private void ItemsList_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
				{
						base.OnPreviewMouseLeftButtonDown (e);

						if (e.OriginalSource is FrameworkElement fe)
						{
								dragData = fe.DataContext;
								var data = (ToolItem)dragData;
								if (data.Name == "Page")
								{
										this.NewPageCommand.Execute (null);
										return;
								}
								DragDrop.DoDragDrop (this, dragData, DragDropEffects.Move);
						}
				}

				private void ListBox_DragEnter(object sender, DragEventArgs e)
				{
						if (e.Data.GetDataPresent (typeof (ListBoxItem)))
						{
								e.Effects = DragDropEffects.Move;
						}
						else
						{
								e.Effects = DragDropEffects.None;
						}
				}

				private void ListBox_DragLeave(object sender, DragEventArgs e)
				{
						e.Effects = DragDropEffects.None;
				}

				private void ListBox_DragOver(object sender, DragEventArgs e)
				{
						if (e.Data.GetDataPresent (typeof (ListBoxItem)))
						{
								e.Effects = DragDropEffects.Move;
						}
						else
						{
								e.Effects = DragDropEffects.None;
						}
				}
		}
}
