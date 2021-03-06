using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripPlanner.Core.Entities;

namespace TripPlanner.Core.Interfaces
{
    public interface ITripRepository
    {
        Response<Trip> Add(Trip trip);
        Response Edit(Trip trip);
        Response Remove(int tripID);
        Response<Trip> Get(int tripID);
        Response<List<Trip>> GetByUser(Guid userID);
    }
}
