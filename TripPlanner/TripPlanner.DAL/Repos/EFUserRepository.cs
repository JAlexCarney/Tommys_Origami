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
    public class EFUserRepository : IUserRepository
    {
        public Response<User> Add(User user)
        {
            throw new NotImplementedException();
        }

        public Response Edit(User user)
        {
            throw new NotImplementedException();
        }

        public Response<User> Get(int userID)
        {
            throw new NotImplementedException();
        }

        public Response Remove(int userID)
        {
            throw new NotImplementedException();
        }
    }
}
