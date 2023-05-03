using Jamesnet.Wpf.Controls;
using objective.Core.Test;
using System.Windows;

namespace objective.Forms.UI.Views
{
        public class MainWindow : ObjectiveWindow
        {

                static MainWindow()
                {
                        DefaultStyleKeyProperty.OverrideMetadata(typeof(MainWindow), new FrameworkPropertyMetadata(typeof(MainWindow)));
                }
        }
}
