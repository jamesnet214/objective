using Jamesnet.Wpf.Controls;
using Jamesnet.Wpf.Mvvm;
using objective.Core.Names;
using Prism.Ioc;
using Prism.Regions;

namespace objective.Forms.Local.ViewModels
{
        public partial class MainViewModel : ObservableBase, IViewLoadable
        {
                private readonly IRegionManager _regionManager;
                private readonly IContainerProvider _containerProvider;

                public MainViewModel(IRegionManager regionManager, IContainerProvider containerProvider)
                {
                        _regionManager = regionManager;
                        _containerProvider = containerProvider;
                }

                public void OnLoaded(IViewable view)
                {
                        IRegion mainRegion = _regionManager.Regions[RegionNameManager.MainRegion];
                        IViewable loginContent = _containerProvider.Resolve<IViewable>(ContentNameManager.Main);

                        if (!mainRegion.Views.Contains(loginContent))
                        {
                                mainRegion.Add(loginContent);
                        }
                        mainRegion.Activate(loginContent);
                }
        }
}
