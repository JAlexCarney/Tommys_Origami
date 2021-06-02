using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripPlanner.Core.Entities;

namespace TripPlanner.Core.Interfaces
{
    public interface IUserRepository
    {
        Response<User> Add(User user);
        Response Edit(User user);
        Response Remove(int userID);
        Response<User> Get(int userID);
    }
}
