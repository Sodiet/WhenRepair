using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NUnit.Framework;

namespace WhenRepair.Application
{
    [TestFixture]
    public class Test
    {
        [Test]
        public async Task Fias()
        {
            var results = await new FiasClient().Get("region", null);

            Console.Write(JsonConvert.SerializeObject(results, Formatting.Indented));
            Assert.That(results, Is.Not.Empty);
        }

        [Test]
        public async Task House()
        {
            var result = await new HouseSearcher().Search(Guid.Parse("92b30014-4d52-4e2e-892d-928142b924bf"),
                                                          Guid.Parse("2763c110-cb8b-416a-9dac-ad28a55b4402"),
                                                          Guid.Parse("95e3c889-e00a-41a2-aa26-bac735271d82"),
                                                          Guid.Parse("8882d015-1fc8-9f80-98ab-82eeb8a70232"));

            Console.Write(JsonConvert.SerializeObject(result, Formatting.Indented));
            Assert.That(result.Length, Is.EqualTo(1));
        }

        [Test]
        public async Task Repair()
        {
            var result = await new RepairSearcher().Search("ул. Анри Барбюса 6");

            Console.Write(JsonConvert.SerializeObject(result, Formatting.Indented));
            Assert.That(result.Length, Is.EqualTo(3));
        }

        [Test]
        public async Task ServiceByYear()
        {
            var serviceDatases = await new HouseServicesByYearDataExtractor().Extract(new Uri("https://www.reformagkh.ru/overhaul/overhaul/services/3175614"));

            Console.Write(JsonConvert.SerializeObject(serviceDatases, Formatting.Indented));
            Assert.That(serviceDatases["2023"].Length, Is.EqualTo(2));
            Assert.That(serviceDatases["2043"].Length, Is.EqualTo(7));
        }

        [Test]
        public async Task HouseCommon()
        {
            var result = await new HouseCommonDataExtractor().Extract(new Uri("https://www.reformagkh.ru/overhaul/overhaul/view/3175614"));
            
            Assert.That(result.NextWorkYear, Is.Not.Null);
            Console.Write(JsonConvert.SerializeObject(result, Formatting.Indented));
        }

        [Test]
        public async Task Autocomplete()
        {
            var result = await new HouseAutocomplieteProvider().Get("ухтомского 3");
            
            Console.Write(JsonConvert.SerializeObject(result, Formatting.Indented));
            Assert.That(result.TotalCount, Is.EqualTo(26));
        }

        [Test]
        public async Task Geocoding_FromCoordinates()
        {
            var result = await new Geocoder().Get(new GeoCoordinate{Latitude = "60.15423675", Longitude = "59.96771635"});
            
            Assert.That(result, Is.Not.Null);
            Console.Write(result);
        }
    }

    public class AddressExtractor
    {
        private static readonly Regex patternCity = new Regex(@"г\.\s*(?<city>[\w\-а-яё]+)", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        private static readonly Regex steetCity = new Regex(@"ул\.\s*(?<street>[\w\-а-яё\s]+),", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        private static readonly Regex houseCity = new Regex(@",\s*(?:д.)?\s*(?<house>[\d\w/]+$)", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        
        
        public Address Extract(string line)
        {
            var result = new Address();
            var city = patternCity.Match(line);
            var street = steetCity.Match(line);
            var house = houseCity.Match(line);
            result.City = city.Success ? city.Groups["city"].Value : "";
            result.Street = street.Success ? street.Groups["street"].Value : "";
            result.House = house.Success ? house.Groups["house"].Value : "";
            return result;
        }
    }
}