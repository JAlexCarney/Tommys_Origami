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
    [Route("[controller]")]
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
        public IActionResult Get(int id)
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
        public IActionResult GetAll(int id)
        {
            var result = _destinationRepository.GetAll();

            if (result.Success)
            {
                return View(result.Data);
            }
            return BadRequest(result.Message);
        }

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
                //return View(result.Data);
                return RedirectToAction("GetAll");

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
        [HttpPost]
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
                //return Remove(result.Data);
                return View(result.Data);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [Route("remove/{id}")]
        [HttpPost]
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
