using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using CsQuery;

namespace WhenRepair.Application
{
    public class RepairSearcher
    {        
        private static readonly Uri homeUri = new Uri("https://www.reformagkh.ru/");
        private static readonly HttpClient httpClient = new HttpClient
        {
            BaseAddress = new Uri("https://www.reformagkh.ru/search/houses"),
        };
        
        public async Task<HouseSummery[]> Search(string query = "")
        {
            var responseMessage = await httpClient.GetAsync($"?query={query}&kr=on");
            responseMessage.EnsureSuccessStatusCode();
            var content = await responseMessage.Content.ReadAsStringAsync();
            
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