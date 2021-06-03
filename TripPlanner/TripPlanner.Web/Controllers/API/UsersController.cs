using Microsoft.AspNetCore.Authorization;
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
    public class UsersController : Controller
    {
        private readonly IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet(Name = "GetUser")] 
        [Route("{id}")]
        public IActionResult GetUser(Guid id)
        {
            var result = _userRepository.Get(id);

            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        [HttpPost]
        public IActionResult AddUser(User user)
        {
            if (ModelState.IsValid)
            {
                Response<User> result = _userRepository.Add(user);

                if (result.Success)
                {
                    return CreatedAtRoute(nameof(GetUser), new { id = result.Data.UserID }, user);
                }
                return BadRequest(result.Message);
            }
            return BadRequest(ModelState);
        }

        [HttpPut]
        public IActionResult EditUser(User user)
        {
            if (!_userRepository.Get(user.UserID).Success)
            {
                return NotFound($"User {user.UserID} not found");
            }

            if (ModelState.IsValid)
            {
                var result = _userRepository.Edit(user);

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
        public IActionResult RemoveUser(Guid id)
        {
            if (!_userRepository.Get(id).Success)
            {
                return NotFound($"User {id} not found");
            }

            var result = _userRepository.Remove(id);

            if (result.Success)
            {
                return Ok();
            }
            return BadRequest(result.Message);
        }

    }
}
