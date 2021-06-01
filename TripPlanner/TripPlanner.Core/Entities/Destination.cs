using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripPlanner.Core.Entities
{
    public class Destination
    {
        public int DestinationID { get; set; }
        public string City { get; set; }
        public string? StateProvince { get; set; }
        public string Country { get; set; }
    }
}
