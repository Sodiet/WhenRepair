using System.Threading.Tasks;
using System.Web.Mvc;
using WhenRepair.Application;

namespace Webmap.Controllers
{
    public class AddressController: Controller
    {
        private static readonly Geocoder geocoder = new Geocoder();
        private static readonly HouseAutocomplieteProvider autocomplieteProvider = new HouseAutocomplieteProvider();
        
        [HttpGet]
        public async Task<ActionResult> Get(string latitude, string longitude)
        {
            var address = await geocoder.Get(new GeoCoordinate {Latitude = latitude, Longitude = longitude});

            return Json(address, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<ActionResult> Autocomplete(string term)
        {
            var result = await autocomplieteProvider.Get(term);

            return Json(result.Addresses, JsonRequestBehavior.AllowGet);
        }
    }
}