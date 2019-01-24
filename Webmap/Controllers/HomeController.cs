using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Webmap.Models;

namespace Webmap.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            var model = new AddressesModel()
            {
                AddressDictionary = new Dictionary<int, List<Address>>()
                {
                    {
                        2017, new List<Address>()
                        {
                            new Address()
                            {
                                Id = Guid.NewGuid(),
                                Date = 2017,
                                Latitude = 56.838219,
                                Longitude = 60.611162
                            },

                            new Address()
                            {
                                Id = Guid.NewGuid(),
                                Date = 2017,
                                Latitude = 56.838694,
                                Longitude = 60.610303
                            },

                            new Address()
                            {
                                Id = Guid.NewGuid(),
                                Date = 2017,
                                Latitude = 56.838503,
                                Longitude = 60.612221
                            },

                            new Address()
                            {
                                Id = Guid.NewGuid(),
                                Date = 2017,
                                Latitude = 56.837500,
                                Longitude = 60.613203
                            }
                        }
                    },

                    {
                        2018, new List<Address>()
                        {
                            new Address()
                            {
                                Id = Guid.NewGuid(),
                                Date = 2018,
                                Latitude = 56.839789,
                                Longitude = 60.612187

                            },

                            new Address()
                            {
                                Id = Guid.NewGuid(),
                                Date = 2018,
                                Latitude = 56.839657,
                                Longitude = 60.610701
                            },

                            new Address()
                            {
                                Id = Guid.NewGuid(),
                                Date = 2018,
                                Latitude = 56.840168,
                                Longitude = 60.610642
                            }
                        }
                    },

                }
            };

            return View(model);
        }
    }
}