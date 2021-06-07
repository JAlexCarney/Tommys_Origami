using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripPlanner.Core.DTOs;
using TripPlanner.Core.Entities;

namespace TripPlanner.Core.Interfaces
{
    public interface IReportsRepository
    {
        Response<List<TopRatedDestinations>> GetTopRatedDestintions();
        Response<List<MostVisitedDestinations>> GetMostVisitedDestintions();
        Response<List<MostReviewedDestinations>> GetMostRatedDestintions();
    }
}
