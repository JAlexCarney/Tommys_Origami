using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TripPlanner.Core;
using TripPlanner.Core.Entities;
using TripPlanner.Core.Interfaces;
using TripPlanner.Web.Models;

namespace TripPlanner.Web.Controllers.API
{
	[ApiController]
	[Route("api/[controller]")]
	public class DestinationTripsController : Controller
    {
		private readonly IDestinationTripRepository _destinationTripRepository;

		public DestinationTripsController(IDestinationTripRepository destinationTripRepository)
		{
			_destinationTripRepository = destinationTripRepository;
        }

        [HttpGet(Name = "GetDestinationTripsByTrip")]
        [Route("/api/destinationtrips/{tripID}")]
        public IActionResult GetDestinationTripsByTrip(int tripID)
        {
            var result = _destinationTripRepository.GetByTrip(tripID);

            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        [HttpGet(Name = "GetDestinationTrip")]
        public IActionResult GetDestinationTrip(DestinationTripModel model)
        {
            if (ModelState.IsValid)
            {
                var result = _destinationTripRepository.Get(model.DestinationID, model.TripID);

                if (result.Success)
                {
                    return Ok(result.Data);
                }
                return BadRequest(result.Message);
            }
            return BadRequest(ModelState);
        }

        [HttpPost]
        [Route("/api/destinationtrips")]
        public IActionResult AddDestinationTrips(DestinationTrip destinationTrip)
        {
            if (ModelState.IsValid)
            {
                Response<DestinationTrip> result = _destinationTripRepository.Add(destinationTrip);

                if (result.Success)
                {
                    return CreatedAtRoute(nameof(GetDestinationTripsByTrip), new { id = result.Data.TripID }, destinationTrip);
                }
                return BadRequest(result.Message);
            }
            return BadRequest(ModelState);
        }

        [HttpPut]
        public IActionResult EditDestinationTrips(DestinationTrip destinationTrip)
        {
            if (!_destinationTripRepository.Get(destinationTrip.DestinationID, destinationTrip.TripID).Success)
            {
                return NotFound($"Destination {destinationTrip.DestinationID} could not be found for Trip {destinationTrip.TripID}");
            }

            if (ModelState.IsValid)
            {
                var result = _destinationTripRepository.Edit(destinationTrip);

                if (result.Success)
                {
                    return Ok();
                }
                return BadRequest(result.Message);
            }
            return BadRequest(ModelState);
        }

        [HttpDelete]
        public IActionResult RemoveDestinationTrips(DestinationTripModel model)
        {
            if (!_destinationTripRepository.Get(model.DestinationID, model.TripID).Success)
            {
                return NotFound($"Destination {model.DestinationID} could not be found for Trip {model.TripID}");

            }

            var result = _destinationTripRepository.Remove(model.DestinationID, model.TripID);

            if (result.Success)
            {
                return Ok();
            }
            return BadRequest(result.Message);
        }
    }
}
