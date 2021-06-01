using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripPlanner.Core.Entities
{
    public class Review
    {
        public int DestinationID { get; set; }
        public int UserID { get; set; }
        public string Description { get; set; }
        public decimal Rating { get; set; }

    }
}
