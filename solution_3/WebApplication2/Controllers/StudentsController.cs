using Microsoft.AspNetCore.Mvc;
using WebApplication2.Models;
using WebApplication2.Services;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IDbService _service;
        public StudentsController(IDbService cdService)
        {
            _service = cdService;
        }

        [HttpGet]
        public IActionResult GetStudents()
        {
            return Ok(_service.GetStudents());
        }

        [HttpGet("{studentId}")]
        public IActionResult GetStudent(int studentId)
        {
            return Ok(_service.GetStudent(studentId));
        }

        [HttpPost]
        public IActionResult CreateStudent([FromBody]Student newStudent)
        {
            if (_service.AddStudent(newStudent))
            {
                return Ok("Student added");
            }
            return BadRequest("Student is already in database");
        }

        [HttpPut("{studentId}")]
        public IActionResult PutStudent([FromBody]Student student)
        {
            if (_service.UpdateStudent(student) != null)
            {
                return Ok(_service.UpdateStudent(student));
            }
            return BadRequest("Student is already in database");
        }

        [HttpDelete("{studentId}")]
        public IActionResult DeleteStudent(int studentId)
        {
            if (_service.DeleteStudent(studentId))
            {
                return Ok();
            }
            else
            {
                return BadRequest("Cannot drop student from database");
            }
        } 
    }
}
