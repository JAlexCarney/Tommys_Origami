using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripPlanner.Core.Entities
{
    public class Review
    {
        // Tabel Properties
        public int DestinationID { get; set; }
        [Required]
        public Guid UserID { get; set; }
        public string Description { get; set; }
        public decimal Rating { get; set; }

        // Navigation Properties
        public User User { get; set; }
        public Destination Destination { get; set; }
    }
}
