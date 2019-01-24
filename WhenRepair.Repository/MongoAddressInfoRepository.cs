using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using WhenRepair.Application;
using WhenRepair.Repository.Types;

namespace WhenRepair.Repository
{
    public class MongoAddressInfoRepository : IAddressInfoRepository
    {
        private readonly IMongoClient client;

        private IMongoCollection<AddressInfo> Addresses =>
            client.GetDatabase("repairs-db").GetCollection<AddressInfo>("addresses");


        private IMongoCollection<RepairInfo> Repair =>
            client.GetDatabase("repairs-db").GetCollection<RepairInfo>("repair");

        public MongoAddressInfoRepository()
            : this(@"mongodb://repairsdb:repairsdb123@rc1b-m0oi3y1rbilpukba.mdb.yandexcloud.net:27018/repairs-db?readPreference=primary")
        { }

        public MongoAddressInfoRepository(string connectionString)
        {
            client = new MongoClient(connectionString);
        }

        public async Task<IDictionary<string, int>> GetYears()
        {
            var res = Addresses
                .Find(FilterDefinition<AddressInfo>.Empty)
                .ToEnumerable();

            return res.SelectMany(a => a.Works)
                .Select(w => $"{w.PlannedStartYear}-{w.PlannedEndYear}")
                .GroupBy(w => w)
                .ToDictionary(w => w.Key, w => w.Count());
        }

        public Task<List<GeoCoordinate>> GetBuildingsByYears(string years)
        {
            var parts = years.Split('-').Select(int.Parse).ToArray();
            return GetBuildingsByYears(parts[0], parts[1]);
        }

        private async Task<List<GeoCoordinate>> GetBuildingsByYears(int start, int end)
        {
            var res = Addresses.Find(
                    Builders<AddressInfo>.Filter.And(
                        Builders<AddressInfo>.Filter.Where(info => info.Works.Any(w => w.PlannedStartYear == start)),
                        Builders<AddressInfo>.Filter.Where(info => info.Works.Any(w => w.PlannedEndYear == end))
                    ))
                .Project(a => new GeoCoordinate { Latitude = a.Lattitude.ToString(CultureInfo.InvariantCulture), Longitude = a.Longitude.ToString(CultureInfo.InvariantCulture) });
            return await res.ToListAsync();
        }

        public async Task Save(string id, RepairData data)
        {
            await Repair.DeleteOneAsync(Builders<RepairInfo>.Filter.Where(info => info.Key == id));
            await Repair.InsertOneAsync(new RepairInfo { Key = id, RepairData = data });
        }
    }
}