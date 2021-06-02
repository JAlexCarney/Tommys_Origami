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

        public Response<Review> Get(int destinationID, int userID)
        {
            throw new NotImplementedException();
        }

        public Response<List<Review>> GetByDestination(int destinationID)
        {
            throw new NotImplementedException();
        }

        public Response<List<Review>> GetByUser(int userID)
        {
            throw new NotImplementedException();
        }

        public Response Remove(int destinationID, int userID)
        {
            throw new NotImplementedException();
        }
    }
}
