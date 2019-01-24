using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using CsQuery;

namespace WhenRepair.Application
{
    public class HouseSearcher
    {
        private static readonly Uri homeUri = new Uri("https://www.reformagkh.ru/");
        private static readonly HttpClient httpClient = new HttpClient();
        
        public async Task<HouseSummery[]> Search(Guid region, Guid settlement, Guid street, Guid house)
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, new Uri("https://www.reformagkh.ru/search/houses-advanced"))
            {
                Content = new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    {"region-guid", region.ToString()},
                    {"settlement-guid", settlement.ToString()},
                    {"street-guid", street.ToString()},
                    {"house-guid", house.ToString()},
                })
            };

            var responseMessage = await httpClient.SendAsync(requestMessage);
            responseMessage.EnsureSuccessStatusCode();
            var content = (await responseMessage.Content.ReadAsStringAsync()).Replace("\r\n", "");

            var dom = CQ.Create(content);
            return dom.Select(".grid a").Select(x => new HouseSummery
            {
                ServicesUri = new Uri(homeUri, x.GetAttribute("href")),
                PasportUri = new Uri(homeUri, x.GetAttribute("href").Replace("services", "view")),
                Title = x.FirstChild.NodeValue
            }).ToArray();
        } 
        
    }
}