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
    public class ADOReportsRepository : IReportsRepository
    {
        public Response<List<Review>> GetMostRatedDestintions()
        {
            throw new NotImplementedException();
        }

        public Response<List<Review>> GetMostVisitedDestintions()
        {
            throw new NotImplementedException();
        }

        public Response<List<Review>> GetTopRatedDestintions()
        {
            throw new NotImplementedException();
        }
    }
}
