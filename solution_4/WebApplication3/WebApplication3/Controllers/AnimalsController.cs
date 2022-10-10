using Microsoft.AspNetCore.Mvc;
using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using WebApplication3.DataAccess;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnimalsController : ControllerBase
    {
        private readonly IAnimalsDataAccess _service;

        public AnimalsController(IAnimalsDataAccess cdService)
        {
/*            SqlConnection con = new SqlConnection("");
            SqlCommand com = con.CreateCommand();*/
            _service = cdService;
        }

        [HttpGet]
        public IActionResult GetAnimals([FromQuery] string orderBy)
        {
            if (orderBy == null){
                orderBy = "Name";
            }
            _service.GetAnimal(orderBy);
            return Ok();
        }

/*        [HttpGet]
        public IActionResult GetAnimal()
        {
            _service.GetAnimal("name");
            return Ok();
        }*/

        [HttpDelete]
        public IActionResult DeleteAnimal(int id)
        {
            _service.DeleteAnimal(id);
            return Ok();
        }
        [HttpPost]
        public IActionResult CreateAnimal([FromBody] Animal animal)
        {
            try 
            {
                _service.CreateAnimal(animal);
            }
            catch (Exception) {
                return BadRequest("Entered data is not valid!");
            }
            return Ok("Animal created!");
           
        }

        [HttpPost("{idAnimal}")]
        public IActionResult UpdateAnimal([FromRoute] int idAnimal, [FromBody] Animal animal)
        {
            try { _service.ChangeAnimal(idAnimal, animal); }
            catch (Exception)
            {
                return NotFound($"No such animal found with ID {idAnimal} or data are not valid!");
            }
            return Ok("Succsesfully changed!");
        }
    }
}
