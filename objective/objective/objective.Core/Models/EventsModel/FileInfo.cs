using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace objective.Core.Models.EventsModel
{
		public class FileInfo
		{
				public string Path { get; set; }
				public string Name { get; set; }
				public string Data { get; set; }

				public bool IsForcedSave { get; set; }
				public FileInfo(string path, string Name, string Data, bool IsForcedSave = false)
				{
						this.Path=path;
						this.Name = Name;
						this.Data = Data;
						this.IsForcedSave = IsForcedSave;
				}
				public FileInfo() { }
		}
}
