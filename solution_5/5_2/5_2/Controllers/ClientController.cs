using _5_2.Interfaces;
using _5_2.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace _5_2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private IClientDbRepository _controller;
        public ClientController(IClientDbRepository controller)
        {
            _controller = controller;
        }

        [HttpDelete("{idClient}")]
        public async Task<IActionResult> Delete(int idClient)
        {

            try
            {
                await _controller.DeleteClient(idClient);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }

            return Ok();
        }
    }
}
