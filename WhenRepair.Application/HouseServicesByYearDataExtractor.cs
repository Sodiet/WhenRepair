using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using CsQuery;

namespace WhenRepair.Application
{
    public class HouseServicesByYearDataExtractor
    {
        private static readonly HttpClient client = new HttpClient();

        public async Task<Dictionary<string, ServiceData[]>> Extract(Uri serviceUri)
        {
            var responseMessage = await client.GetAsync(new Uri(serviceUri, "?finished=0&byYear=1"));
            responseMessage.EnsureSuccessStatusCode();
            var content = await responseMessage.Content.ReadAsStringAsync();

            var cq = CQ.Create(content);

            return cq.Select(".location_lists > div")
                     .Select(x => Tuple.Create(ExtractYear(x), ExtractServiceData(x.Cq())))
                     .ToDictionary(x => x.Item1, x => x.Item2);
        }

        private static string ExtractYear(IDomObject dom)
        {
            return dom.Cq().Find(".location_list_header").Elements.First()[4].Cq().Text().Trim();
        }

        private static ServiceData[] ExtractServiceData(CQ dom)
        {
            return dom.Find(".grid > table > tbody").Select(x =>
                      {
                          var main = x.Cq().Find("td");
                          var price = x.Cq().Find("tbody .leaders span");
                          return new ServiceData
                          {
                              Title = main[0].Cq().Text().Trim(),
                              EndWorkYear = main[1].Cq().Text().Trim(),
                              PriceByPlan = price[1].Cq().Text().Trim(),
                              PriceByOrder = price[3].Cq().Text().Trim(),
                              PriceByAct = price[5].Cq().Text().Trim(),
                              OrderDate = price[15].Cq().Text().Trim()
                          };
                      })
                      .ToArray();
        }
    }
}