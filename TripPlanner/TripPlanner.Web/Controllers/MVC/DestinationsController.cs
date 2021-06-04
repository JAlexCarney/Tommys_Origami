using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TripPlanner.Core;
using TripPlanner.Core.Entities;
using TripPlanner.Core.Interfaces;

namespace TripPlanner.Web.Controllers.MVC
{
    public class DestinationsController : Controller
    {
        private readonly IDestinationRepository _destinationRepository;

        public DestinationsController(IDestinationRepository destinationRepository)
        {
            _destinationRepository = destinationRepository;
        }

        //get, get all, add, edit, remove

        [HttpGet(Name = "GetDestination")]
        [Route("{id}")]
        public IActionResult GetDestination(int id)
        {
            var result = _destinationRepository.Get(id);

            if (result.Success)
            {
                return View(result.Data);
            }
            return BadRequest(result.Message);
        }

        [HttpGet(Name = "GetAllDestination")]
        [Route("list")]
        public IActionResult GetAllDestinations(int id)
        {
            var result = _destinationRepository.GetAll();

            if (result.Success)
            {
                return View(result.Data);
            }
            return BadRequest(result.Message);
        }

        /*
        [HttpPost]
        public IActionResult AddDestination(Destination destination)
        {
            if (ModelState.IsValid)
            {
                Response<Destination> result = _destinationRepository.Add(destination);

                if (result.Success)
                {
                    return CreatedAtRoute(nameof(GetDestination), new { id = result.Data.DestinationID }, destination);
                }
                return BadRequest(result.Message);
            }
            return BadRequest(ModelState);
        }
        */

        // need / before?
        [Route("add")]
        [HttpGet]
        public IActionResult Add()
        {
            var model = new Destination();
            return View(model);
        }


        [Route("add")]
        [HttpPost]
        public IActionResult Add(Destination model)
        {
            var result = _destinationRepository.Add(model);

            if (result.Success)
            {
                return View(result.Data);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [Route("edit/{id}")]
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var result = _destinationRepository.Get(id);

            if (result.Success)
            {
                return View(result.Data);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [Route("edit/{id}")]
        [HttpPut]
        public IActionResult Edit(Destination model)
        {
            var result = _destinationRepository.Edit(model);

            if (result.Success)
            {
                return RedirectToAction("GetAll");
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [Route("remove/{id}")]
        [HttpGet]
        public IActionResult Remove(int id)
        {
            var result = _destinationRepository.Get(id);

            if (result.Success)
            {
                return Remove(result.Data);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [Route("remove/{id}")]
        [HttpDelete]
        public IActionResult Remove(Destination model)
        {
            var result = _destinationRepository.Remove(model.DestinationID);

            if (result.Success)
            {
                return RedirectToAction("GetAll");
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
