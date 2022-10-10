using _5_2.Interfaces;
using _5_2.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _5_2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TripsController : ControllerBase
    {

        private ITripDbRepository _controller;

        public TripsController(ITripDbRepository controller)
        {
            _controller = controller;
        }

        [HttpGet]
        public async Task<IActionResult> GetTrips()
        {
            IEnumerable<Trip> trips;
            try
            {
                trips = await _controller.GetTripsAsync();
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
            return Ok(trips);
        }

        [HttpGet("{idTrip}/{Clients}")]
        public IActionResult AddClient(int idTrip, AddTripClient clientTrip)
        {
            try
            {
                _controller.AssignClientToTrip(idTrip, clientTrip);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
            return Ok();
        }
    }
}
