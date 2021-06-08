using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TripPlanner.Core.Interfaces;

namespace TripPlanner.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : Controller
    {
        private readonly IReportsRepository _reportsRepository;

        public ReportsController(IReportsRepository reportsRepository)
        {
            _reportsRepository = reportsRepository;
        }

        //toprated, mostvisited, mostrated

        [Route("toprated")]
        [HttpGet]
        public IActionResult TopRated()
        {
            var result = _reportsRepository.GetTopRatedDestintions();

            if (result.Success)
            {
                return Ok(result.Data);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [Route("mostvisited")]
        [HttpGet]
        public IActionResult MostVisited()
        {
            var result = _reportsRepository.GetMostVisitedDestintions();

            if (result.Success)
            {
                return Ok(result.Data);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [Route("mostrated")]
        [HttpGet]
        public IActionResult MostRated()
        {
            var result = _reportsRepository.GetMostRatedDestintions();

            if (result.Success)
            {
                return Ok(result.Data);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
    }
}
