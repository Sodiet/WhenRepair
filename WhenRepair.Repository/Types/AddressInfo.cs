using System.Collections.Generic;
using MongoDB.Bson;
using WhenRepair.Application;

namespace WhenRepair.Repository.Types
{
    public class AddressInfo
    {
        public ObjectId _id { private get; set; }
        public string id { get; set; }

        public string DistrictName { get; set; }
        public string Address { get; set; }
        public List<WorksInfo> Works { get; set; }

        public decimal Longitude { get; set; }
        public decimal Lattitude { get; set; }
    }

    public class RepairInfo
    {
        public ObjectId _id { private get; set; }
        public string Key { get; set; }
        public RepairData RepairData;
    }
}
