using System.Collections.Generic;
using MongoDB.Bson;

namespace WhenRepair.Repository.Types
{
	public class AddressInfo
	{
		public ObjectId _id { private get; set; }

		public string DistrictName { get; set; }
		public string Address { get; set; }
		public List<WorksInfo> Works { get; set; }

		public decimal Longitude { get; set; }
		public decimal Lattitude { get; set; }
	}
}
