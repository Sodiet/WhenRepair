using System;
using System.Net.Http;
using System.Threading.Tasks;
using CsQuery;

namespace WhenRepair.Application
{
    public class HouseCommonDataExtractor
    {
        private static readonly HttpClient client = new HttpClient();
        
        public async Task<HouseCommonData> Extract(Uri passportUri)
        {
            var responseMessage = await client.GetAsync(passportUri);
            responseMessage.EnsureSuccessStatusCode();

            var content = await responseMessage.Content.ReadAsStringAsync();
            var cq = CQ.Create(content);

            var bulletInfo = cq.Select(".bulletin div span");
            var table = cq.Select(".table-details td span");

            return new HouseCommonData
            {
                StartingYear = table[1].FirstChild.NodeValue.Trim(),
                PamentByMeter = bulletInfo[0].FirstChild.NodeValue.Trim(),
                Collected = bulletInfo[1].FirstChild.NodeValue.Trim(),
                Dept = bulletInfo[2].FirstChild.NodeValue.Trim(),
                Spent = bulletInfo[3].FirstChild.NodeValue.Trim(),
                SpentSubsidy = bulletInfo[4].FirstChild.NodeValue.Trim(),
                Balance = bulletInfo[5].FirstChild.NodeValue.Trim(),
                NextWorkYear = bulletInfo[8].FirstChild.NodeValue.Trim()
            };
        }
    }
}