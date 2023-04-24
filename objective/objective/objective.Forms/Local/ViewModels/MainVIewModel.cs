using Jamesnet.Wpf.Controls;
using Jamesnet.Wpf.Mvvm;
using Microsoft.Win32;
using objective.Core.Test;
using objective.Core.Events;
using objective.Core.Names;
using Prism.Events;
using Prism.Ioc;
using Prism.Regions;
using System;
using System.IO;
using System.Threading.Tasks;

namespace objective.Forms.Local.ViewModels
{
		public partial class MainViewModel : ObservableBase, IViewLoadable, IViewClosedable
        {
                private readonly IRegionManager _regionManager;
                private readonly IContainerProvider _containerProvider;
				private readonly IEventAggregator _eh;
				public MainViewModel(IRegionManager regionManager, 
                                                IContainerProvider containerProvider,
                                                IEventAggregator eh)
                {
                        _regionManager = regionManager;
                        _containerProvider = containerProvider;
                        _eh = eh;
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

				public void OnCloesd()
				{
						_eh.GetEvent<ClosedEvent> ().Publish ();
				}
		}
}
