using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripPlanner.Core;
using TripPlanner.Core.Entities;
using TripPlanner.Core.Interfaces;

namespace TripPlanner.DAL.Repos
{
    public class EFTripRepository : ITripRepository
    {
        public Response<Trip> Add(Trip trip)
        {
            throw new NotImplementedException();
        }

        public Response Edit(Trip trip)
        {
            throw new NotImplementedException();
        }

        public Response<Trip> Get(int tripID)
        {
            throw new NotImplementedException();
        }

        public Response<List<Trip>> GetByUser(int userID)
        {
            throw new NotImplementedException();
        }

        public Response Remove(int tripID)
        {
            throw new NotImplementedException();
        }
    }
}
