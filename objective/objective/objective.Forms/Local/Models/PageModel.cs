using CommunityToolkit.Mvvm.ComponentModel;
using Jamesnet.Wpf.Mvvm;
using Newtonsoft.Json;
using objective.Core;
using objective.Forms.UI.Units;
using objective.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace objective.Forms.Local.Models
{
		public partial class PageModel : ObservableBase
		{
				[ObservableProperty]
				private ObservableCollection<ReportObject> _reportSource;

				public PageModel()
				{
						this.ReportSource = new ();
				}

				public ReportModel Save()
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

						return m;
				}
				public void GetReportSource(ReportModel objData)
				{
						List<ReportObject> list = new ();
						foreach (var data in objData.Objects)
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
						this.ReportSource = new ObservableCollection<ReportObject> (list);
				}
		}
}
