using Jamesnet.Wpf.Controls;
using objective.Core.Names;
using objective.Forms.UI.Views;
using Prism.Ioc;
using Prism.Modularity;

namespace objective.Properties
{
        public class ViewModules : IModule
        {
                public void OnInitialized(IContainerProvider containerProvider)
                {
                }

                public void RegisterTypes(IContainerRegistry containerRegistry)
                {
                        containerRegistry.RegisterSingleton<IViewable, MainContent>(ContentNameManager.Main);
                }
        }
}
