using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripPlanner.Core.Entities;

namespace TripPlanner.Core.Interfaces
{
    public interface IReportsRepository
    {
        Response<List<Review>> GetTopRatedDestintions();
        Response<List<Review>> GetMostVisitedDestintions();
        Response<List<Review>> GetMostRatedDestintions();
    }
}
