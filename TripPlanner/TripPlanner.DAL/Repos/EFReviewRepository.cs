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
    public class EFReviewRepository : IReviewRepository
    {
        public Response<Review> Add(Review review)
        {
            throw new NotImplementedException();
        }

        public Response Edit(Review review)
        {
            throw new NotImplementedException();
        }

        public Response<Review> Get(int destinationID, Guid userID)
        {
            throw new NotImplementedException();
        }

        public Response<List<Review>> GetByDestination(int destinationID)
        {
            throw new NotImplementedException();
        }

        public Response<List<Review>> GetByUser(Guid userID)
        {
            throw new NotImplementedException();
        }

        public Response Remove(int destinationID, Guid userID)
        {
            throw new NotImplementedException();
        }
    }
}
