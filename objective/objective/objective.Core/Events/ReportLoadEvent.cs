using objective.Core.Models.EventsModel;
using Prism.Events;

namespace objective.Core.Events
{
		public class ReportLoadEvent : PubSubEvent<FileInfo>
		{
		}
}
