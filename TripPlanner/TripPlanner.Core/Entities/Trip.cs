using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripPlanner.Core.Entities
{
    public class Trip
    {
        // Tabel Properties
        public int TripID { get; set; }
        [ForeignKey("User")]
        public Guid UserID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime ProjectedEndDate { get; set; }
        public DateTime? ActualEndDate { get; set; }
        public bool Booked { get; set; }

        // Navigation Properties
        public List<Destination> Destinations { get; set; }
        public User User { get; set; }
    }
}
