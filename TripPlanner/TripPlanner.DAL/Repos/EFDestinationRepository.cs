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
    public class EFDestinationRepository : IDestinationRepository
    {
        private readonly TripPlannerAppContext _context;

        public EFDestinationRepository()
        {
            _context = TripPlannerAppContext.GetDBContext();
        }

        public EFDestinationRepository(TripPlannerAppContext context)
        {
            _context = context;
        }
        public Response<Destination> Add(Destination destination)
        {
            var response = new Response<Destination>();
            var validationResponse = IsValidAdd(destination);
            if (!validationResponse.Success)
            {
                response.Message = validationResponse.Message;
                return response;
            }
            Destination added;
            try
            {
                added = _context.Destination.Add(destination).Entity;
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

        public Response Edit(Destination destination)
        {
            var response = new Response();
            var validationResponse = IsValidEdit(destination);
            if (!validationResponse.Success)
            {
                return validationResponse;
            }
            Destination editing;
            try
            {
                editing = _context.Destination.Find(destination.DestinationID);
                if (editing == null)
                {
                    response.Message = "Failed to find destination with given Id";
                    return response;
                }
                editing.DestinationID = destination.DestinationID;
                editing.City = destination.City;
                editing.StateProvince = destination.StateProvince;
                editing.Country= destination.Country;
                _context.SaveChanges();

            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                return response;
            }

            return response;
        }

        public Response<Destination> Get(int destinationID)
        {
            var response = new Response<Destination>();
            Destination found;
            try
            {
                found = _context.Destination.Find(destinationID);
                if (found == null)
                {
                    response.Message = $"Trip with id {destinationID} not found";
                    return response;
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                return response;
            }
            response.Data = found;
            return response;
        }

        public Response<List<Destination>> GetAll()
        {
            var response = new Response<List<Destination>>();
            List<Destination> listDestinations;
            try
            {
                listDestinations = _context.Destination.ToList();
                if (!listDestinations.Any())
                {
                    response.Message = $"Could not find any destinations";
                    return response;
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                return response;
            }

            response.Data = listDestinations;
            return response;
        }

        public Response Remove(int destinationID)
        {
            Destination toRemove;
            var response = new Response();
            try
            {
                toRemove = _context.Destination.Find(destinationID);
                if (toRemove == null)
                {
                    response.Message = "Failed to find Destination with given Id";
                    return response;
                }
                var destinationTripsToRemove = _context.DestinationTrip.Where(dt => dt.DestinationID == destinationID);
                if (destinationTripsToRemove.Any())
                {
                    foreach (DestinationTrip destinationTrip in destinationTripsToRemove)
                    {
                        _context.DestinationTrip.Remove(destinationTrip);
                    }
                }
                var reviewToRemove = _context.Review.Where(r => r.DestinationID == destinationID);
                if (reviewToRemove.Any())
                {
                    foreach (Review review in reviewToRemove)
                    {
                        _context.Review.Remove(review);
                    }
                }
                _context.Destination.Remove(toRemove);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                return response;
            }
            return response;
        }

        private static Response IsValidAdd(Destination destination)
        {
            var response = new Response();
            if (string.IsNullOrEmpty(destination.City))
            {
                response.Message = "City name is required";
            }
            else if (string.IsNullOrEmpty(destination.Country))
            {
                response.Message = "The country is required";
            }

            return response;
        }

        private Response IsValidEdit(Destination destination)
        {
            var response = new Response();
            if (string.IsNullOrEmpty(destination.City))
            {
                response.Message = "City name is required";
            }
            else if (string.IsNullOrEmpty(destination.Country))
            {
                response.Message = "The country is required";
            }
            Destination found = _context.Destination.Find(destination.DestinationID);
            if (found == null)
            {
                response.Message = "Failed to find Destination Id";
                return response;
            }
            return response;
        }
    }
}
