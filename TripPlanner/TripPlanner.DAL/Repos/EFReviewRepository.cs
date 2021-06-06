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
    public class EFReviewRepository : IReviewRepository
    {

        private readonly TripPlannerAppContext _context;

        public EFReviewRepository()
        {
            _context = TripPlannerAppContext.GetDBContext();
        }

        public EFReviewRepository(TripPlannerAppContext context)
        {
            _context = context;
        }


        public Response<Review> Add(Review review)
        {
            var response = new Response<Review>();
            var validationResponse = IsValid(review);
            if (!validationResponse.Success)
            {
                response.Message = validationResponse.Message;
                return response;
            }
            Review added;
            try
            {
                added = _context.Review.Add(review).Entity;
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                return response;
            }
            response.Data = added;
            return response;
            //throw new NotImplementedException();
        }

        public Response Edit(Review review)
        {
            var response = new Response();
            var validationResponse = IsValid(review);
            if (!validationResponse.Success)
            {
                return validationResponse;
            }
            Review editing;
            try
            {
                editing = _context.Review.FirstOrDefault(r =>
                    r.DestinationID == review.DestinationID
                    && r.UserID == review.UserID);
                if (editing == null)
                {
                    response.Message = "Failed to find Review with given IDs";
                    return response;
                }
                editing.Description = review.Description;
                _context.SaveChanges();
                editing.Rating = review.Rating;
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                return response;
            }
            return response;
            //throw new NotImplementedException();
        }

        public Response<Review> Get(int destinationID, Guid userID)
        {
            var response = new Response<Review>();
            Review found;
            try
            {
                found = _context.Review.FirstOrDefault(r =>
                    r.DestinationID == destinationID
                    && r.UserID == userID);
                if (found == null)
                {
                    response.Message = "Failed to find Review with given IDs";
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                return response;
            }
            response.Data = found;
            return response;
            //throw new NotImplementedException();
        }

        public Response<List<Review>> GetByDestination(int destinationID)
        {
            var response = new Response<List<Review>>();
            List<Review> found;
            try
            {
                found = _context.Review.Where(r =>
                    r.DestinationID == destinationID).ToList();
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                return response;
            }
            response.Data = found;
            return response;
            //throw new NotImplementedException();
        }

        public Response<List<Review>> GetByUser(Guid userID)
        {
            var response = new Response<List<Review>>();
            List<Review> found;
            try
            {
                found = _context.Review.Where(r =>
                    r.UserID == userID).ToList();
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                return response;
            }
            response.Data = found;
            return response;
            //throw new NotImplementedException();
        }

        public Response Remove(int destinationID, Guid userID)
        {
            Review toRemove;
            var response = new Response();
            try
            {
                toRemove = _context.Review.FirstOrDefault(r =>
                    r.DestinationID == destinationID
                    && r.UserID == userID);
                if (toRemove == null)
                {
                    response.Message = "Failed to find Review with given ID";
                    return response;
                }
                _context.Review.Remove(toRemove);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                return response;
            }
            return response;
            //throw new NotImplementedException();
        }

        private static Response IsValid(Review review)
        {
            var response = new Response();
            if (review.DestinationID == 0)
            {
                response.Message = "DestinationID is required";
            }
            else if (review.UserID == Guid.Empty)
            {
                response.Message = "UserID is required";
            }
            else if (string.IsNullOrEmpty(review.Description))
            {
                response.Message = "Description is required";
            }
            else if (review.Rating == 0)
            {
                response.Message = "Rating is required";
            }
            else if (review.Rating < 0)
            {
                response.Message = "Rating cannot be negative";
            }
            else if (review.Rating > 5)
            {
                response.Message = "Rating cannot be above 5.0";
            }
            return response;
        }
    }
}
