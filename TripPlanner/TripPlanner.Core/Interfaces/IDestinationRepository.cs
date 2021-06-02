using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripPlanner.Core.Entities;

namespace TripPlanner.Core.Interfaces
{
    public interface IDestinationRepository
    {
        Response<Destination> Get(int destinationID);
        Response<List<Destination>> GetAll();
        Response<Destination> Add(Destination destination);
        Response Edit(Destination destination);
        Response Remove(int destinationID);
    }
}
