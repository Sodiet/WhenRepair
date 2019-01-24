using System.Threading.Tasks;

namespace WhenRepair.Application
{
    public class RepairDataExtractor
    {
        private static readonly HouseCommonDataExtractor commonDataExtractor = new HouseCommonDataExtractor();
        private static readonly HouseServicesByYearDataExtractor houseServicesByYearDataExtractor = new HouseServicesByYearDataExtractor();
        
        public async Task<RepairData> Extract(HouseSummery summery)
        {
            var commonTask = commonDataExtractor.Extract(summery.PasportUri);
            var serviceTask = houseServicesByYearDataExtractor.Extract(summery.ServicesUri);
            
            await Task.WhenAll(commonTask, serviceTask);

            return new RepairData
            {
                House = commonTask.Result,
                ServicesByYear = serviceTask.Result
            };
        } 
    }
}