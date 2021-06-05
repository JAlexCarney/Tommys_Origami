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
    public class EFDestinationTripRepository : IDestinationTripRepository
    {
        private readonly TripPlannerAppContext _context;

        public EFDestinationTripRepository()
        {
            _context = TripPlannerAppContext.GetDBContext();
        }

        public EFDestinationTripRepository(TripPlannerAppContext context)
        {
            _context = context;
        }

        public Response<DestinationTrip> Add(DestinationTrip destinationTrip)
        {
            var response = new Response<DestinationTrip>();
            var validationResponse = IsValid(destinationTrip);
            if (!validationResponse.Success)
            {
                response.Message = validationResponse.Message;
                return response;
            }
            DestinationTrip added;
            try
            {
                added = _context.DestinationTrip.Add(destinationTrip).Entity;
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

        public Response Edit(DestinationTrip destinationTrip)
        {
            var response = new Response();
            var validationResponse = IsValid(destinationTrip);
            if (!validationResponse.Success)
            {
                return validationResponse;
            }
            DestinationTrip editing;
            try
            {
                editing = _context.DestinationTrip.FirstOrDefault(dt => 
                    dt.DestinationID == destinationTrip.DestinationID
                    && dt.TripID == destinationTrip.TripID);
                if (editing == null)
                {
                    response.Message = "Failed to find DestinationTrip with given Id";
                    return response;
                }
                editing.Description = destinationTrip.Description;
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                return response;
            }
            return response;
        }

        public Response<DestinationTrip> Get(int destinationID, int tripID)
        {
            var response = new Response<DestinationTrip>();
            DestinationTrip found;
            try
            {
                found = _context.DestinationTrip.FirstOrDefault(dt =>
                    dt.DestinationID == destinationID
                    && dt.TripID == tripID);
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                return response;
            }
            response.Data = found;
            return response;
        }

        public Response<List<DestinationTrip>> GetByTrip(int tripID)
        {
            var response = new Response<List<DestinationTrip>>();
            List<DestinationTrip> found;
            try
            {
                found = _context.DestinationTrip.Where(dt =>
                    dt.TripID == tripID).ToList();
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                return response;
            }
            response.Data = found;
            return response;
        }

        public Response Remove(int destinationID, int tripID)
        {
            DestinationTrip toRemove;
            var response = new Response();
            try
            {
                toRemove = _context.DestinationTrip.FirstOrDefault(dt =>
                    dt.DestinationID == destinationID
                    && dt.TripID == tripID);
                if (toRemove == null)
                {
                    response.Message = "Failed to find DestinationTrip with given Id";
                    return response;
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                return response;
            }
            return response;
        }

        private static Response IsValid(DestinationTrip destinationTrip) 
        {
            var response = new Response();
            if (destinationTrip.TripID == 0)
            {
                response.Message = "TripID is required";
            }
            else if (destinationTrip.DestinationID == 0)
            {
                response.Message = "Destination is required";
            }
            return response;
        }
    }
}
