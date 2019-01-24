using System.Collections.Generic;

namespace WhenRepair.Repository.Types
{
	public class WorksInfo
	{
		public int LastCapitalWorksYear { get; set; }
		public List<string> WorkDescriptions { get; set; }
		public int PlannedStartYear { get; set; }
		public int PlannedEndYear { get; set; }
	}
}