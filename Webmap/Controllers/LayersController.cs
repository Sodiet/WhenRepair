﻿using System.Threading.Tasks;
using System.Web.Mvc;
using WhenRepair.Application;
using WhenRepair.Repository;

namespace Webmap.Controllers
{
    public class LayersController : Controller
    {
        //private static readonly IAddressInfoRepository addressInfoRepository =

        private IAddressInfoRepository Get()
        {
            return new MongoAddressInfoRepository();
        }
            

        public async Task<ActionResult> GetYears()
        {
            //var dictionary = await Get().GetYears();

            var years = new[]
            {
                "2015-2017",
                "2018-2020",
                "2021-2023",
                "2024-2026",
                "2027-2029",
                "2030-2032",
                "2033-2035",
                "2036-2038",
                "2039-2041",
                "2042-2044",
            };

            //var years = dictionary.Keys;

            return Json(years, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> Getinfo()
        {

            var info = new[]
            {
                "Задолженности"
            };

            return Json(info, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> GetLayer(string years)
        {
//            var geoCoordinate = new[]
//            {
//                new GeoCoordinate()
//                {
//                    Latitude = "56.837500",
//                    Longitude = "60.613203"
//
//                },
//                new GeoCoordinate()
//                {
//                    Latitude = "56.838219",
//                    Longitude = "60.611162"
//                },
//
//                new GeoCoordinate()
//                {
//                    Latitude = "56.838694",
//                    Longitude = "60.610303"
//                },
//
//                new GeoCoordinate()
//                {
//                    Latitude = "56.838503",
//                    Longitude = "60.612221"
//                },
//            };

            var geoCoordinates = await Get().GetBuildingsByYears(years);

            return Json(geoCoordinates, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> GetInfoLayer()
        {
            var geoCoordinates = await Get().GetInfoLayerRemote();

            return Json(geoCoordinates, JsonRequestBehavior.AllowGet);
        }
    }
}