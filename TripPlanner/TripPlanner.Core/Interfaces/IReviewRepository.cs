using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripPlanner.Core.Entities;

namespace TripPlanner.Core.Interfaces
{
    public interface IReviewRepository
    {
        Response<Review> Add(Review review);
        Response Edit(Review review);
        Response Remove(int destinationID, int userID);
        Response<Review> Get(int destinationID, int userID);
        Response<List<Review>> GetByDestination(int destinationID);
        Response<List<Review>> GetByUser(int userID);
    }
}
