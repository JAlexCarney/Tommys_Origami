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
    public class EFDestinationRepository : IDestinationRepository
    {
        public Response<Destination> Add(Destination destination)
        {
            throw new NotImplementedException();
        }

        public Response Edit(Destination destination)
        {
            throw new NotImplementedException();
        }

        public Response<Destination> Get(int destinationID)
        {
            throw new NotImplementedException();
        }

        public Response<List<Destination>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Response Remove(int destinationID)
        {
            throw new NotImplementedException();
        }
    }
}
