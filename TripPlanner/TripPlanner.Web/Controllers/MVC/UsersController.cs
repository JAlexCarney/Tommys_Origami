using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TripPlanner.Core.Entities;
using TripPlanner.Core.Interfaces;

namespace TripPlanner.Web.Controllers.MVC
{
    [Route("[controller]")]
    public class UsersController : Controller
    {
        private readonly IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [Route("index")]
        public IActionResult Index()
        {
            var result = _userRepository.GetAll();

            if (result.Success)
            {
                return View(result.Data);
            }

            return View("error", result);
        }

        [HttpGet(Name = "Get")]
        [Route("{id}")]
        public IActionResult Get(Guid id)
        {
            var result = _userRepository.Get(id);

            if (result.Success)
            {
                return View(result.Data);
            }
            return BadRequest(result.Message);
        }

        [Route("add")]
        [HttpGet]
        public IActionResult Add()
        {
            var model = new User();
            return View(model);
        }


        [Route("add")]
        [HttpPost]
        public IActionResult Add(User model)
        {
            model.DateCreated = DateTime.Now;
            var result = _userRepository.Add(model);

            if (result.Success)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [Route("edit/{id}")]
        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            var result = _userRepository.Get(id);

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
        public IActionResult Edit(User model)
        {
            var result = _userRepository.Edit(model);

            if (result.Success)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [Route("remove/{id}")]
        [HttpGet]
        public IActionResult Remove(Guid id)
        {
            var result = _userRepository.Get(id);

            if (result.Success)
            {
                return View(result.Data); 
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [Route("remove/{id}")]
        [HttpPost]
        public IActionResult Remove(User model)
        {
            var result = _userRepository.Remove(model.UserID);

            if (result.Success)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
    }
}
