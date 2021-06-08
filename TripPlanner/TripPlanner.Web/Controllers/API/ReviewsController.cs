using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TripPlanner.Core;
using TripPlanner.Core.Entities;
using TripPlanner.Core.Interfaces;

namespace TripPlanner.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : Controller
    {

        private readonly IReviewRepository _reviewRepository;

        public ReviewsController(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        //add, edit, remove, get, getbyuser, getbydestination

        [HttpGet(Name = "GetReviewsByDestination"), Authorize]
        [Route("/api/reviews/getreviewsbydestination/{destinationID}")]
        public IActionResult GetReviewsByDestination(int destinationID)
        {
            var result = _reviewRepository.GetByDestination(destinationID);

            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }


        [HttpGet(Name = "GetReviewsByUser"), Authorize]
        [Route("/api/reviews/getreviewsbyuser/{userID}")]
        public IActionResult GetReviewsByUser(Guid userID)
        {
            var result = _reviewRepository.GetByUser(userID);

            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        [HttpGet(Name = "GetReview"), Authorize]
        [Route("/api/reviews/getreview")]
        public IActionResult GetReview(Review model)
        {
            if (ModelState.IsValid)
            {
                var result = _reviewRepository.Get(model.DestinationID, model.UserID);

                if (result.Success)
                {
                    return Ok(result.Data);
                }
                return BadRequest(result.Message);
            }
            return BadRequest(ModelState);
        }

        [HttpPost, Authorize]
        [Route("/api/reviews")]
        public IActionResult AddReview(Review review)
        {
            if (ModelState.IsValid)
            {
                Response<Review> result = _reviewRepository.Add(review);

                if (result.Success)
                {
                    //check
                    return CreatedAtRoute(nameof(GetReviewsByDestination), new { id = result.Data.DestinationID }, review);
                }
                return BadRequest(result.Message);
            }
            return BadRequest(ModelState);
        }

        [HttpPut, Authorize]
        public IActionResult EditReviews(Review review )
        {
            if (!_reviewRepository.Get(review.DestinationID, review.UserID).Success)
            {
                return NotFound($"Review by user {review.UserID} could not be found for destination {review.DestinationID}");
            }

            if (ModelState.IsValid)
            {
                var result = _reviewRepository.Edit(review);

                if (result.Success)
                {
                    return Ok();
                }
                return BadRequest(result.Message);
            }
            return BadRequest(ModelState);
        }

        [HttpDelete, Authorize]
        //check reviewmodel
        public IActionResult RemoveReviews(Review model)
        {
            if (!_reviewRepository.Get(model.DestinationID, model.UserID).Success)
            {
                return NotFound($"Review by user {model.UserID} could not be found for destination {model.DestinationID}");
            }

            var result = _reviewRepository.Remove(model.DestinationID, model.UserID);

            if (result.Success)
            {
                return Ok();
            }
            return BadRequest(result.Message);
        }
    }
}
