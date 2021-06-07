using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripPlanner.Core.DTOs
{
    public class MostVisitedDestinations
    {
        public int DestinationID { get; set; }
        public string City { get; set; }
        public string StateProvince { get; set; }
        public string Country { get; set; }
        public int TotalVisitors { get; set; }
    }
}
