using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Webmap.Models
{
    public class Address
    {
        public Guid Id { get; set; }
        public int Date { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}