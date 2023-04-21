using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Jamesnet.Wpf.Mvvm;
using Microsoft.Win32;
using Newtonsoft.Json;
using objective.Core;
using objective.Core.Events;
using objective.Forms.Local.Models;
using objective.Models;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace objective.Forms.Local.ViewModels
{
		public partial class MainContentViewModel : ObservableBase
		{
				[ObservableProperty]
				private ReportObject _selectedObject;

				[ObservableProperty]
				private List<ToolItem> _tools;

				[ObservableProperty]
				private ObservableCollection<PageModel> pages;

				private readonly IEventAggregator _eh;
				public MainContentViewModel(IEventAggregator eh)
				{
						_eh = eh;
						_eh.GetEvent<ReportLoadEvent> ().Subscribe (FileDataLoad);
						//_eh.GetEvent<ClosedEvent> ().Subscribe (()=>
						//{
						//		this.Save ();
						//});
						Tools = GetTools ();
						this.Pages = new ();
				}

				private string FilePath;
				private string FileLoadName;

				private void GetReportSource(string base64)
				{
						byte[] bytes = Convert.FromBase64String (base64);
						string json = Encoding.UTF8.GetString (bytes);

						var objs = JsonConvert.DeserializeObject<List<ReportModel>> (json);
						if (objs.Count > 0)
								this.Pages.Clear ();
						foreach (var obj in objs)
						{
								this.Pages.Add (new PageModel ());
						}
						Task.Run (() =>
						{
								Thread.Sleep (1000);

								Application.Current.Dispatcher.Invoke (() =>
								{
										for (int i = 0; i < this.Pages.Count; i++)
										{
												this.Pages[i].GetReportSource (objs[i]);
										}
								}, null);
						});
				}

				private void FileDataLoad(Core.Models.EventsModel.FileInfo fileInfo)
				{
						FilePath = fileInfo.Path;
						FileLoadName = fileInfo.Name;

						if (!String.IsNullOrWhiteSpace (fileInfo.Data))
								Application.Current.Dispatcher.Invoke (() =>
								{
										// UI 스레드에서 실행할 작업
										GetReportSource (fileInfo.Data);
								}, null);
				}

				[RelayCommand]
				private void Load()
				{
						OpenFileDialog ofd = new ();
						ofd.InitialDirectory = Environment.CurrentDirectory;
						ofd.Filter = "objective files(*.objective) | *.objective";
						ofd.Multiselect = false;

						if (ofd.ShowDialog () == true)
						{
								FilePath = Path.GetDirectoryName (ofd.FileName);
								FileLoadName = Path.GetFileName (ofd.FileName);
								string line = null;
								using (var reader = new StreamReader (ofd.FileName))
								{
										// 파일 내용 한 줄씩 읽기
										line = reader.ReadToEnd ();
								}
								_eh.GetEvent<ReportLoadEvent> ().Publish (new Core.Models.EventsModel.FileInfo (FilePath, FileLoadName, line));
						}
				}

				[RelayCommand]
				private void NewPage()
				{
						this.Pages.Add (new PageModel ());
				}

				[RelayCommand]
				private void Save()
				{
						List<ReportModel> reportModels = new List<ReportModel> ();
						foreach (var page in Pages)
						{
								reportModels.Add (page.Save ());
						}

						string json = JsonConvert.SerializeObject (reportModels);
						byte[] bytes = Encoding.UTF8.GetBytes (json);
						string base64 = Convert.ToBase64String (bytes);

						_eh.GetEvent<ReportSaveEvent> ().Publish (new Core.Models.EventsModel.FileInfo (FilePath, FileLoadName, base64));
				}

				private List<ToolItem> GetTools()
				{
						List<ToolItem> source = new ();
						source.Add (new ToolItem ("Page"));
						source.Add (new ToolItem ("Title"));
						source.Add (new ToolItem ("Table"));
						source.Add (new ToolItem ("Horizontal Line"));
						source.Add (new ToolItem ("Image"));
						return source;
				}

				[RelayCommand]
				private void SelectItem(ReportObject item)
				{
						SelectedObject = item;
				}
		}
}
