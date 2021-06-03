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
    [ApiController]
    [Route("api/[controller]")]
    public class TripsController : Controller
    {
        private readonly ITripRepository _tripRepository;

        public TripsController(ITripRepository tripRepository) 
        {
            _tripRepository = tripRepository;
        }

        [HttpGet]
        [Route("byuser/{id}")]
        public IActionResult GetTripsByUser(Guid id)
        {
            var result = _tripRepository.GetByUser(id);

            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        [HttpGet(Name = "GetTrip")]
        [Route("{id}")]
        public IActionResult GetTrip(int id)
        {
            var result = _tripRepository.Get(id);

            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        [HttpPost]
        public IActionResult AddTrip(Trip trip)
        {
            if (ModelState.IsValid)
            {
                Response<Trip> result = _tripRepository.Add(trip);

                if (result.Success)
                {
                    return CreatedAtRoute(nameof(GetTrip), new { id = result.Data.TripID }, trip);
                }
                return BadRequest(result.Message);
            }
            return BadRequest(ModelState);
        }

        [HttpPut]
        public IActionResult EditTrip(Trip trip)
        {
            if (!_tripRepository.Get(trip.TripID).Success)
            {
                return NotFound($"Trip {trip.TripID} not found");
            }

            if (ModelState.IsValid)
            {
                var result = _tripRepository.Edit(trip);

                if (result.Success)
                {
                    return Ok();
                }
                return BadRequest(result.Message);
            }
            return BadRequest(ModelState);
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult RemoveUser(int id)
        {
            if (!_tripRepository.Get(id).Success)
            {
                return NotFound($"Trip {id} not found");
            }

            var result = _tripRepository.Remove(id);

            if (result.Success)
            {
                return Ok();
            }
            return BadRequest(result.Message);
        }
    }
}
