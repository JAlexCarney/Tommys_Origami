using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripPlanner.Core.DTOs
{
    public class DestinationTripsWithCity
    {
        public int DestinationID { get; set; }
        public int TripID { get; set; }
        public string CityCountry { get; set; }
        public string Description { get; set; }
    }
}
