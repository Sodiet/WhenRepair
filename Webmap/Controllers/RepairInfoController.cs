using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using WhenRepair.Application;

namespace Webmap.Controllers
{
    public class RepairInfoController: Controller
    {
        private static readonly Geocoder geocoder = new Geocoder();
        private static readonly RepairSearcher repairSearcher = new RepairSearcher();
        private static readonly RepairDataExtractor dataExtractor = new RepairDataExtractor();
        
        public async Task<ActionResult> Get(string latitude, string longitude)
        {
            var address = await geocoder.Get(latitude, longitude);
            if(address == null)
                return new HttpNotFoundResult();

            var searchQuery = $"{address.State} {address.City} {address.Street} {address.House}";

            var houseSummeries = await repairSearcher.Search(searchQuery);
            if (houseSummeries.Length == 0)
                return Json(new {address}, JsonRequestBehavior.AllowGet);
            
            var repairData = await dataExtractor.Extract(houseSummeries.First());
            return Json(new {address, repair = repairData}, JsonRequestBehavior.AllowGet);
        }
    }
}