using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Jamesnet.Wpf.Controls;
using Jamesnet.Wpf.Mvvm;
using Microsoft.Win32;
using Newtonsoft.Json;
using objective.Core;
using objective.Forms.Local.Models;
using objective.Models;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace objective.Forms.Local.ViewModels
{
		public partial class MainContentViewModel : ObservableBase, IViewLoadable
		{
				[ObservableProperty]
				private bool isLoad = false;
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
						Tools = GetTools ();
						this.Pages = new ();
				}

				public void OnLoaded(IViewable smartWindow)
				{
						Task.Run (() =>
						{
								Load ();
						});
				}

				private string FilePath;
				private string FileLoadName;

				private void GetReportSource(string base64)
				{
						byte[] bytes = Convert.FromBase64String (base64);
						string json = Encoding.UTF8.GetString (bytes);

						var objs = JsonConvert.DeserializeObject<List<ReportModel>> (json);
						if (objs.Count > 0)
						{
								this.Pages.Clear ();
								IsLoad = true;
						}
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
										IsLoad = false;
								}, null);
						});
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
								IsLoad = false;
								if (!String.IsNullOrWhiteSpace (FilePath))
										Application.Current.Dispatcher.Invoke (() =>
										{
												
												// UI 스레드에서 실행할 작업
												GetReportSource (line);
										}, null);
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

						string PullPath = $@"{FilePath}\{FileLoadName}";

						// 다른이름으로 저장하기 기능 추가구현할때 필요
						//if (fileInfo.IsForcedSave = false)
						//{
						//        if (File.Exists (PullPath))
						//        {
						//                SaveFileDialog sfd = new ();
						//                sfd.InitialDirectory = fileInfo.Path;
						//                sfd.FileName = fileInfo.Name;
						//                if (sfd.ShowDialog () == false)
						//                        return;
						//        }
						//}

						using (StreamWriter sw = new (PullPath, false))
						{
								sw.Write (base64);
						}
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
