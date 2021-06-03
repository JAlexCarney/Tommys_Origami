using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripPlanner.Core.Entities
{
    public class Review
    {
        // Tabel Properties
        public int DestinationID { get; set; }
        public Guid UserID { get; set; }
        public string Description { get; set; }
        public decimal Rating { get; set; }

        // Navigation Properties
        public User User { get; set; }
        public Destination Destination { get; set; }
    }
}
