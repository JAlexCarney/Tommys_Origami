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
    public class EFDestinationTripRepository : IDestinationTripRepository
    {
        public Response<DestinationTrip> Add(DestinationTrip destinationTrip)
        {
            throw new NotImplementedException();
        }

        public Response Edit(DestinationTrip destinationTrip)
        {
            throw new NotImplementedException();
        }

        public Response<DestinationTrip> Get(int destinationID, int tripID)
        {
            throw new NotImplementedException();
        }

        public Response<List<DestinationTrip>> GetByTrip(int tripID)
        {
            throw new NotImplementedException();
        }

        public Response Remove(int destinationID, int tripID)
        {
            throw new NotImplementedException();
        }
    }
}
