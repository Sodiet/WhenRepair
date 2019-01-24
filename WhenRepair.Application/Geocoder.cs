using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace WhenRepair.Application
{
    public class Geocoder
    {
        private static readonly HttpClient client = new HttpClient
        {
            BaseAddress = new Uri("http://nominatim.openstreetmap.org/search")
        };

        public Task<Address> Get(string latitude, string longitude)
        {
            return Get(new GeoCoordinate {Latitude = latitude, Longitude = longitude});
        }
        
        public async Task<GeoCoordinate> Get(string city, string street, string house)
        {
            var query = $"{city}+{street}+{house}";
            var responseMessage = await client.GetAsync($"?format=json&q={query}");
            responseMessage.EnsureSuccessStatusCode();
            var content = await responseMessage.Content.ReadAsStringAsync();

            var json = JArray.Parse(content);

            return json.Count != 0 ? new GeoCoordinate
            {
                Latitude = json.First["lat"].ToString(),
                Longitude = json.First["lon"].ToString()
            } : null;
        }

        public async Task<Address> Get(GeoCoordinate coordinate)
        {
            var query = $"{coordinate.Latitude}+{coordinate.Longitude}";
            var responseMessage = await client.GetAsync($"?format=json&addressdetails=1&q={query}");
            responseMessage.EnsureSuccessStatusCode();
            var content = await responseMessage.Content.ReadAsStringAsync();

            var json = JArray.Parse(content);

            return json.Count != 0 ? new Address
            {
                City = json.First["address"]["city"]?.ToString() ?? "",
                Street = json.First["address"]["road"]?.ToString() ?? "",
                House = json.First["address"]["house_number"]?.ToString() ?? "",
                Postcode = json.First["address"]["postcode"]?.ToString() ?? "",
                State = json.First["address"]["state"]?.ToString() ?? ""
            } : null;
        }
    }
}