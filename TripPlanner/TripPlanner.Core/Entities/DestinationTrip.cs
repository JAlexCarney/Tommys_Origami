using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripPlanner.Core.Entities
{
    public class DestinationTrip
    {
        // Tabel Properties
        public int TripID { get; set; }
        public int DestinationID { get; set; }
        public string Description { get; set; }

        // Navigation Properties
        public Trip Trip { get; set; }
        public Destination Destination { get; set; }
    }
}
