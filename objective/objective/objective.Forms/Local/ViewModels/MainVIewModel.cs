using Jamesnet.Wpf.Controls;
using Jamesnet.Wpf.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace objective.Forms.Local
{
        public partial class MainVIewModel : ObservableBase, IViewLoadable
        {
                public MainVIewModel()
                {

                }
                public void OnLoaded(IViewable smartWindow)
                {
                        throw new NotImplementedException();
                }
        }
}
