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
        private readonly TripPlannerAppContext _context;

        public EFUserRepository() 
        {
            _context = TripPlannerAppContext.GetDBContext();
        }

        public EFUserRepository(TripPlannerAppContext context) 
        {
            _context = context;
        }

        public Response<User> Add(User user)
        {
            User added;
            var response = new Response<User>();
            try
            {
                added = _context.User.Add(user).Entity;
                _context.SaveChanges();
            }
            catch (Exception ex) 
            {
                response.Message = ex.Message;
                return response;
            }
            response.Data = added;
            return response;
        }

        public Response Edit(User user)
        {
            User editing;
            var response = new Response();
            try
            {
                editing = _context.User.Find(user.UserID);
                if (editing == null) 
                {
                    response.Message = "Failed to find User with given Id";
                }
                editing.Username = user.Username;
                editing.Password = user.Password;
                editing.Email = user.Email;
                editing.DateCreated = user.DateCreated;
                _context.SaveChanges();

            }
            catch (Exception ex) 
            {
                response.Message = ex.Message;
                return response;
            }

            return response;
        }

        public Response<User> Get(int userID)
        {
            var response = new Response<User>();
            User found;
            try
            {
                found = _context.User.Find(userID);
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                return response;
            }
            response.Data = found;
            return response;
        }

        public Response Remove(int userID)
        {
            User toRemove;
            var response = new Response();
            try
            {
                toRemove = _context.User.Find(userID);
                if (toRemove == null) 
                {
                    response.Message = "Failed to find User with given Id";
                    return response;
                }
                var reviewsToRemove = _context.Review.Where(r => r.UserID == userID);
                if (reviewsToRemove.Any()) 
                {
                    foreach (Review review in reviewsToRemove) 
                    {
                        _context.Review.Remove(review);
                        _context.SaveChanges();
                    }
                }
                var tripsToRemove = _context.Trip.Where(t => t.UserID == userID);
                if(tripsToRemove.Any())
                {
                    foreach (Trip trip in tripsToRemove)
                    {
                        var destinationTripsToRemove = _context.DestinationTrip.Where(dt => dt.TripID == trip.TripID);
                        if (destinationTripsToRemove.Any()) 
                        {
                            foreach (DestinationTrip destinationTrip in destinationTripsToRemove)
                            {
                                _context.DestinationTrip.Remove(destinationTrip);
                                _context.SaveChanges();
                            }
                        }
                        _context.Trip.Remove(trip);
                        _context.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                return response;
            }
            return response;
        }
    }
}
