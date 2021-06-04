using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TripPlanner.Core;
using TripPlanner.Core.Entities;
using TripPlanner.Core.Interfaces;
using TripPlanner.Web.Models;

namespace TripPlanner.Web.Controllers.API
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {

        private readonly IUserRepository _userRepository;

        public AuthController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost]
        [Route("/api/auth/login")]
        public IActionResult Login(LoginModel loginModel)
        {
            if(loginModel == null)
            {
                return BadRequest("No login credentials provided");
            }
            Response<List<User>> response = _userRepository.GetAll();
            List<User> users;
            if (!response.Success)
            {
                return BadRequest("No users were found");
            }
            users = response.Data;
            if(!users.Any(u => u.Email == loginModel.Identifier && u.Password == loginModel.Password)
                && !users.Any(u => u.Username == loginModel.Identifier && u.Password == loginModel.Password))
            {
                return Unauthorized("Could not locate user");
            }
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("KeyForSignInSecret@1234"));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var tokeOptions = new JwtSecurityToken(
                issuer: "http://localhost:2000",
                audience: "http://localhost:2000",
                claims: new List<Claim>(),
                expires: DateTime.Now.AddMinutes(180),
                signingCredentials: signinCredentials
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
            var user = users.Find(u => u.Email == loginModel.Identifier && u.Password == loginModel.Password);
            if (user == null) 
            {
                user = users.Find(u => u.Username == loginModel.Identifier && u.Password == loginModel.Password);
            }
            return Ok(new { Token = tokenString, UserID = user.UserID.ToString() });
        }
    }
}
