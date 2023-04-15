using objective.Forms.Local.EventArgs;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace objective.Forms.UI.Units
{
        public class Table : ListBox
        {
                public ICommand AreaClickedCommand
                {
                        get { return (ICommand)GetValue(AreaClickedCommandProperty); }
                        set { SetValue(AreaClickedCommandProperty, value); }
                }

                // Using a DependencyProperty as the backing store for AreaClickedCommand.  This enables animation, styling, binding, etc...
                public static readonly DependencyProperty AreaClickedCommandProperty =
                    DependencyProperty.Register("AreaClickedCommand", typeof(ICommand), typeof(Table), new PropertyMetadata(null));


                static Table()
                {
                        DefaultStyleKeyProperty.OverrideMetadata(typeof(Table), new FrameworkPropertyMetadata(typeof(Table)));
                }
                protected override DependencyObject GetContainerForItemOverride()
                {
                        return new TableItem();
                }

                protected override void OnMouseDown(MouseButtonEventArgs e)
                {
                        if (e.OriginalSource is FrameworkElement source)
                        {
                                var tag = source.Tag;
                                if(e.ChangedButton == MouseButton.Left || e.ChangedButton == MouseButton.Right)
                                {
                                        this.AreaClickedCommand?.Execute(new TableCustomArgs()
                                        {
                                                mouseButton =e.ChangedButton,
                                                gridAddType = (string)tag == "RightArea" ? Local.enums.GridAddType.Columns : Local.enums.GridAddType.Rows
                                        });
                                }
                        }
                }
        }
}
