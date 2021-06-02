using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripPlanner.Core.Entities;

namespace TripPlanner.Core.Interfaces
{
    public interface IDestinationTripRepository
    {
        Response<DestinationTrip> Add(DestinationTrip destinationTrip);
        Response Edit(DestinationTrip destinationTrip);
        Response Remove(int destinationID, int tripID);
        Response<DestinationTrip> Get(int destinationID, int tripID);
        Response<List<DestinationTrip>> GetByTrip(int tripID);
    }
}
