using System.Collections.Generic;
using System.Threading.Tasks;
using WhenRepair.Application;

namespace WhenRepair.Repository
{
	public interface IAddressInfoRepository
	{
		Task<IDictionary<string, int>> GetYears();
		Task<List<GeoCoordinate>> GetBuildingsByYears(string years);
	}
}
