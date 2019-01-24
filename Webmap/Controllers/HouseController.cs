using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using WhenRepair.Application;

namespace Webmap.Controllers
{
    public class HouseController : Controller
    {
        private static readonly AddressExtractor addressExtractor = new AddressExtractor();
        private static readonly RepairSearcher repairSearcher = new RepairSearcher();
        private static readonly Geocoder geocoder = new Geocoder();
        
        public async Task<ActionResult> Search(string address)
        {
            var houseSummeries = await repairSearcher.Search(address);
            var extract = addressExtractor.Extract(houseSummeries.FirstOrDefault()?.Title ?? address);
            var geoCoordinate = await geocoder.Get(extract.City, extract.Street, extract.House);
            return Json(geoCoordinate, JsonRequestBehavior.AllowGet);
        }
    }

}