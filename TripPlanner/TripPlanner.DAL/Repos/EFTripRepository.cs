using Microsoft.EntityFrameworkCore;
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
    public class EFTripRepository : ITripRepository
    {
        private readonly TripPlannerAppContext _context;

        public EFTripRepository()
        {
            _context = TripPlannerAppContext.GetDBContext();
        }

        public EFTripRepository(TripPlannerAppContext context)
        {
            _context = context;
        }

        public Response<Trip> Add(Trip trip)
        {
            var response = new Response<Trip>();
            var validationResponse = IsValidAdd(trip);
            if (!validationResponse.Success)
            {
                response.Message = validationResponse.Message;
                return response;
            }
            Trip added;
            try
            {
                added = _context.Trip.Add(trip).Entity;
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

        public Response Edit(Trip trip)
        {
            var response = new Response();
            var validationResponse = IsValidEdit(trip);
            if (!validationResponse.Success)
            {
                return validationResponse;
            }
            Trip editing;
            try
            {
                editing = _context.Trip.Find(trip.TripID);
                if (editing == null)
                {
                    response.Message = "Failed to find Trip with given Id";
                    return response;
                }
                editing.UserID = trip.UserID;
                editing.StartDate = trip.StartDate;
                editing.Name = trip.Name;
                editing.ProjectedEndDate = trip.ProjectedEndDate;
                editing.ActualEndDate = trip.ActualEndDate;
                editing.IsBooked = trip.IsBooked;
                _context.SaveChanges();

            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                return response;
            }

            return response;
        }

        public Response<Trip> Get(int tripID)
        {
            var response = new Response<Trip>();
            Trip found;
            try
            {
                found = _context.Trip.Find(tripID);
                if (found == null) 
                {
                    response.Message = $"Trip with id {tripID} not found";
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

        public Response<List<Trip>> GetByUser(Guid userID)
        {
            var response = new Response<List<Trip>>();
            List<Trip> found = new List<Trip>();
            try
            {
                List<Trip> listTrips = _context.Trip.AsNoTracking().ToList();
                foreach (var t in listTrips)
                {
                    if (t.UserID == userID)
                    {
                        found.Add(t);
                    }
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

        public Response Remove(int tripID)
        {
            Trip toRemove;
            var response = new Response();
            try
            {
                toRemove = _context.Trip.Find(tripID);
                if (toRemove == null)
                {
                    response.Message = "Failed to find Trip with given Id";
                    return response;
                }
                var destinationTripsToRemove = _context.DestinationTrip.Where(dt => dt.TripID == tripID);
                if (destinationTripsToRemove.Any())
                {
                    foreach (DestinationTrip destinationTrip in destinationTripsToRemove)
                    {
                        _context.DestinationTrip.Remove(destinationTrip);
                    }
                }
                _context.Trip.Remove(toRemove);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                return response;
            }
            return response;
        }

        private static Response IsValidAdd(Trip trip)
        {
            var response = new Response();
            //userid must exist, point to valid user
            if (trip.UserID == Guid.Empty)
            {
                response.Message = "UserID is required";
            }
            //validation to ensure userid points to valid user ?
            //start date must exist, must be in future
            else if ((trip.StartDate == new DateTime()))
            {
                response.Message = "Start date is required";
            }
            else if (trip.StartDate.Subtract(DateTime.Now).Ticks < 0)
            {
                response.Message = "Start date must be in the future";
            }
            //projected end date must exist, must come after start date
            else if ((trip.ProjectedEndDate == new DateTime()))
            {
                response.Message = "Projected end date is required";
            }
            else if ((trip.ProjectedEndDate.Subtract(trip.StartDate)).Ticks < 0)
            {
                response.Message = "Projected end date must come after Start date";
            }
            //actual end date must be null at time of trip scheduling
            else if ((trip.ActualEndDate != null))
            {
                response.Message = "Actual end date is required to be null";
            }
            return response;
        }

        private static Response IsValidEdit(Trip trip)
        {
            var response = new Response();
            //cannot edit user
            //startdate must exist, must be in future
            if ((trip.StartDate == new DateTime()))
            {
                response.Message = "Start date is required";
            }
            else if ((trip.StartDate.Subtract(DateTime.Now)).Ticks < 0)
            {
                response.Message = "Start date must be in the future";
            }
            //projected end date must exist, must come after start date
            else if ((trip.ProjectedEndDate == new DateTime()))
            {
                response.Message = "Projected end date is required";
            }
            else if ((trip.ProjectedEndDate.Subtract(trip.StartDate)).Ticks < 0)
            {
                response.Message = "Projected end date must come after Start date";
            }
            //actual end date (if not null) must come after start date
            else if ((trip.ActualEndDate != null) && ((trip.ActualEndDate.Value.Subtract(trip.StartDate)).Ticks < 0))
            {
                response.Message = "Actual end date must come after start date";
            }
            return response;
        }
    }
}
