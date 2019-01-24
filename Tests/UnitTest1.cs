using System;
using NUnit.Framework;
using WhenRepair.Repository;

namespace Tests
{
	[TestFixture]
	public class UnitTest1
	{
		[Test]
		public void TestSearch()
		{
			var repo = new MongoAddressInfoRepository(
				@"mongodb://repairs-db:KW4KrvNhq6bGJlblPQMlKTUsx3N53HI8Cm9nxs6KSMlHT7QJ5ZYdNZH9826BROUI4BMXH3tIIiZVvKIqOORIXA==@repairs-db.documents.azure.com:10255/?ssl=true&replicaSet=globaldb");

			var result = repo.GetYears().GetAwaiter().GetResult();
			foreach (var years in result)
			{
				Console.WriteLine($"{years.Key}:{years.Value}");
			}
		}

		[Test]
		public void TestSearchByYear()
		{
			var repo = new MongoAddressInfoRepository(
				@"mongodb://repairs-db:KW4KrvNhq6bGJlblPQMlKTUsx3N53HI8Cm9nxs6KSMlHT7QJ5ZYdNZH9826BROUI4BMXH3tIIiZVvKIqOORIXA==@repairs-db.documents.azure.com:10255/?ssl=true&replicaSet=globaldb");

			var result = repo.GetBuildingsByYears("2015-2017").GetAwaiter().GetResult();
			foreach (var years in result)
			{
				Console.WriteLine($"{years.Longitude}:{years.Latitude}");
			}
		}
	}
}
