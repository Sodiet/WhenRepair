using System.Collections.Generic;

namespace WhenRepair.Application
{
    public class RepairData
    {
        public HouseCommonData House { get; set; }

        public Dictionary<string, ServiceData[]> ServicesByYear { get; set; }
    }
}