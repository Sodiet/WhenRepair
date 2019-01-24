using System;
using System.Linq;
using System.Threading.Tasks;
using WhenRepair.Repository;

namespace WhenRepair.Application
{
    public class RepairDataExtractor
    {
        private static readonly HouseCommonDataExtractor commonDataExtractor = new HouseCommonDataExtractor();
        private static readonly HouseServicesByYearDataExtractor houseServicesByYearDataExtractor = new HouseServicesByYearDataExtractor();
        private static readonly IAddressInfoRepository addressInfoRepository = new MongoAddressInfoRepository();

        public async Task<RepairData> Extract(HouseSummery summery)
        {
            var commonTask = commonDataExtractor.Extract(summery.PasportUri);
            var serviceTask = houseServicesByYearDataExtractor.Extract(summery.ServicesUri);
            
            await Task.WhenAll(commonTask, serviceTask);

            var data = new RepairData
            {
                House = commonTask.Result,
                ServicesByYear = serviceTask.Result
            };

            var id = summery.PasportUri.Segments.Last();
            await addressInfoRepository.Save(id, data);
            return data;
        } 
    }
}