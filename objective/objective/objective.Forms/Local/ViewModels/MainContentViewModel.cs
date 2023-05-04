using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Jamesnet.Wpf.Controls;
using Jamesnet.Wpf.Mvvm;
using Microsoft.Win32;
using Newtonsoft.Json;
using objective.Core;
using objective.Core.Events;
using objective.Forms.Local.Models;
using objective.Models;
using objective.SampleData;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
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

						this._eh.GetEvent<ClosedEvent> ().Subscribe (() =>
						{
								this.Save ();
						});
				}

				public void OnLoaded(IViewable smartWindow)
				{
						Task.Run (() =>
						{
								OpenFileDialog ofd = new ();
								ofd.InitialDirectory = Environment.CurrentDirectory;
								ofd.Filter = "objective files(*.objective) | *.objective";
								ofd.Multiselect = false;

								if (ofd.ShowDialog () == false)
								{
										FilePath = Environment.CurrentDirectory;
										FileLoadName = "Default.objective";
										string PullPath = $@"{FilePath}\{FileLoadName}";

										using (StreamWriter sw = new (PullPath, false))
										File.Create ("Default.objective");
										Application.Current.Dispatcher.Invoke (() =>
										{
												GetReportSource (EncryptData.Base64);
										}, null);
										return;
								}

								this.FileLoad (ofd);
						});
				}

				private string FilePath;
				private string FileLoadName;

				private void GetReportSource(string base64)
				{
						byte[] bytes = Convert.FromBase64String (base64);
						string json = Encoding.UTF8.GetString (bytes);

						var objs = JsonConvert.DeserializeObject<List<ReportModel>> (json);
						if(objs == null)
						{
								bytes = Convert.FromBase64String (EncryptData.Base64);
								json = Encoding.UTF8.GetString (bytes);

								objs = JsonConvert.DeserializeObject<List<ReportModel>> (json);
						}
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

						if (ofd.ShowDialog () == false)
								return;

						this.FileLoad (ofd);
				}

				private void FileLoad(OpenFileDialog ofd)
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
										GetReportSource (line);
								}, null);
				}

				[RelayCommand]
				private void NewPage()
				{
						this.Pages.Add (new PageModel ());
				}

				[RelayCommand]
				private void RemovePage()
				{
						if(this.Pages.Count == 1)
						{
								this.Pages[0].Clear ();
								return;
						}

						var lastPages = this.Pages.Last ();

						this.Pages.Remove (lastPages);
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
						Clipboard.SetText (base64);
						using (StreamWriter sw =File.CreateText(PullPath))
						{
								sw.Write (base64);
						}
				}

				private List<ToolItem> GetTools()
				{
						List<ToolItem> source = new ();
						source.Add (new ToolItem ("Page"));
						source.Add (new ToolItem ("RemovePage"));
						source.Add (new ToolItem ("Title"));
						source.Add (new ToolItem ("Table"));
						source.Add (new ToolItem ("Horizontal Line"));
						source.Add (new ToolItem ("Image"));
						return source;
				}


				[RelayCommand]
				private void SelectItem(ReportObject item)
				{
						if(isPress == true)
						{
								multiObject.Add (item);
						}
						SelectedObject = item;
				}
				private bool isPress = false;
				List<ReportObject> multiObject = new ();
				[RelayCommand]
				private void MultiSelectItem(bool isPress)
				{
						this.isPress = isPress;
						if (isPress == false)
						{
								this.multiObject.Clear ();
						}
				}
		}
}
