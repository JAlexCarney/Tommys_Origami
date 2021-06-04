using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripPlanner.Core.Entities
{
    public class Destination
    {
        // Tabel Properties
        public int DestinationID { get; set; }
        [Required]
        public string City { get; set; }
        public string StateProvince { get; set; }
        [Required]
        public string Country { get; set; }

        // Navigation Properties
        public List<DestinationTrip> DestinationTrips { get; set; }
        public List<Review> Reviews { get; set; }
    }
}
