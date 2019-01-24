using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WhenRepair.Application
{
    public class FiasClient
    {
        private static readonly HttpClient httpClient = new HttpClient
        {
            BaseAddress = new Uri("https://www.reformagkh.ru/myhouse/list-fias"),
            DefaultRequestHeaders = {{"X-Requested-With", "XMLHttpRequest"}}
        };
        
        public async Task<FiasResult[]> Get(string level, string parent, string term = null)
        {
            var builder = new StringBuilder($"?level={level}");
            builder.Append($"parent={parent ?? ""}");
            if (term != null)
                builder.Append($"term={term}");

            var responseMessage = await httpClient.GetAsync(builder.ToString());
            responseMessage.EnsureSuccessStatusCode();
            var content = await responseMessage.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<FiasResult[]>(content);
        }
    }
}