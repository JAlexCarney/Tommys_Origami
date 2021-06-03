using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripPlanner.Core.Entities
{
    public class User
    {
        // Tabel Properties
        public Guid UserID { get; set; }
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public DateTime DateCreated { get; set; }

        // Navigation Properties
        public List<Trip> Trips { get; set; }
        public List<Review> Reviews { get; set; }
    }
}
