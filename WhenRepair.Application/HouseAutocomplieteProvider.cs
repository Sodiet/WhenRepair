using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WhenRepair.Application
{
    public class HouseAutocomplieteProvider
    {
        private static readonly HttpClient httpClient = new HttpClient
        {
            BaseAddress = new Uri("https://www.reformagkh.ru/search/houses-autocomplete"),
            DefaultRequestHeaders =
            {
                {"X-Requested-With", "XMLHttpRequest"},
                {"User-Agent", "WhenRepair" }
            }
        };
        
        public async Task<AddressAutocomplete> Get(string qurey = "")
        {
            var responseMessage = await httpClient.GetAsync($"?query={qurey}&email=a@ya.ru");
            responseMessage.EnsureSuccessStatusCode();
            var content = await responseMessage.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<AddressAutocomplete>(content);
        } 
    }
}