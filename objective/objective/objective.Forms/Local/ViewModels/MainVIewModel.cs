using Jamesnet.Wpf.Controls;
using Jamesnet.Wpf.Mvvm;
using Microsoft.Win32;
using objective.Core.Events;
using objective.Core.Names;
using Prism.Events;
using Prism.Ioc;
using Prism.Regions;
using System;
using System.Buffers.Text;
using System.IO;

namespace objective.Forms.Local.ViewModels
{
        public partial class MainViewModel : ObservableBase, IViewLoadable
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
						this._eh.GetEvent<ReportSaveEvent> ().Subscribe (ReportSave);
				}

				private void ReportSave(Core.Models.EventsModel.FileInfo fileInfo)
				{
                        string PullPath = $@"{fileInfo.Path}\{fileInfo.Name}";

                        if (fileInfo.IsForcedSave = false)
                        {
                                if (File.Exists (PullPath))
                                {
                                        SaveFileDialog sfd = new ();
                                        sfd.InitialDirectory = fileInfo.Path;
                                        sfd.FileName = fileInfo.Name;
                                        if (sfd.ShowDialog () == false)
                                                return;
                                }
                        }

                        using (StreamWriter sw = new (PullPath))
                        {
                                sw.WriteLine (fileInfo.Data);
                        }
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

						OpenFileDialog ofd = new ();
						ofd.InitialDirectory = Environment.CurrentDirectory;
						ofd.Filter = "objective files(*.objective) | *.objective";
						ofd.Multiselect = false;

						if (ofd.ShowDialog () == true)
						{
								string FilePath = Path.GetDirectoryName (ofd.FileName);
								string FileLoadName = Path.GetFileName (ofd.FileName);
								string line = null;
								using (var reader = new StreamReader (ofd.FileName))
								{
										// 파일 내용 한 줄씩 읽기
										line = reader.ReadToEnd ();
								}
								_eh.GetEvent<ReportLoadEvent> ().Publish (new Core.Models.EventsModel.FileInfo (FilePath, FileLoadName, line));
						}
				}
        }
}
