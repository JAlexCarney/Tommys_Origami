using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripPlanner.Core.Entities
{
    public class DestinationTrip
    {
        // Tabel Properties
        [ForeignKey("Trip")]
        public int TripID { get; set; }
        [ForeignKey("Destination")]
        public int DestinationID { get; set; }
        public string Description { get; set; }

        // Navigation Properties
        public Trip Trip { get; set; }
        public Destination Destination { get; set; }
    }
}
