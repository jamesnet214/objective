using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Jamesnet.Wpf.Mvvm;
using Microsoft.Win32;
using Newtonsoft.Json;
using objective.Core;
using objective.Core.Events;
using objective.Forms.Local.Models;
using objective.Forms.UI.Units;
using objective.Models;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
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
                private ObservableCollection<ReportObject> _reportSource;

                private readonly IEventAggregator _eh;
                public MainContentViewModel(IEventAggregator eh)
                {
                        _eh = eh;
						_eh.GetEvent<ReportLoadEvent> ().Subscribe (FileDataLoad);
						Tools = GetTools();
                        this.ReportSource = new();
                }

				private string FilePath ;
				private string FileLoadName;

				private ObservableCollection<ReportObject> GetReportSource(string base64)
				{
						byte[] bytes = Convert.FromBase64String (base64);
						string json = Encoding.UTF8.GetString (bytes);

						var obj = JsonConvert.DeserializeObject<ReportModel> (json);

						List<ReportObject> list = new ();
						foreach (var data in obj.Objects)
						{
								ReportObject item = null;
								switch (data.Type.Name)
								{
										case "Header":
												item = new Header ().SetProperties (data);
												break;
										case "HorizontalLine":
												item = new HorizontalLine ().SetProperties (data);
												break;
										case "Text":
												item = new Text ().SetProperties (data);
												break;
										case "Table":
												item = new Table ().SetProperties (data);
												break;
										case "Picture":
												item = new Picture ().SetProperties (data);
												break;
								}
								list.Add (item);
						}

						return new ObservableCollection<ReportObject> (list);
				}

				private void FileDataLoad(Core.Models.EventsModel.FileInfo fileInfo)
				{
						FilePath = fileInfo.Path;
						FileLoadName = fileInfo.Name;

						if (!String.IsNullOrWhiteSpace (fileInfo.Data))
								ReportSource = GetReportSource (fileInfo.Data);
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
				private void Save()
				{
						ReportModel m = new ();
						m.Objects = new ();
						foreach (ReportObject data in ReportSource)
						{
								var item = data.GetProperties ();
								m.Objects.Add (item);
						}

						string json = JsonConvert.SerializeObject (m);
						byte[] bytes = Encoding.UTF8.GetBytes (json);
						string base64 = Convert.ToBase64String (bytes);
						Clipboard.SetText (base64);

						_eh.GetEvent<ReportSaveEvent> ().Publish (new Core.Models.EventsModel.FileInfo(FilePath, FileLoadName, base64));
				}
				private List<ToolItem> GetTools()
                {
                        List<ToolItem> source = new();
                        source.Add(new ToolItem("Title"));
                        source.Add(new ToolItem("Table"));
                        source.Add(new ToolItem("Horizontal Line"));
                        source.Add(new ToolItem("Image"));
                        return source;
                }

                [RelayCommand]
                private void SelectItem(ReportObject item)
                {
                        SelectedObject = item;
                }
        }
}
