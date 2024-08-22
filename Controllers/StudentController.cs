using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Won7E1.Data;
using Won7E1.DTOs;
using Won7E1.Models;
using Won7E1.Service;

namespace Won7E1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly StudentService _studentService;

        public StudentController(StudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet("get-all-students")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Student>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<IActionResult> GetAllStudents()
        {
            try
            {
                var studentsList = await _studentService.GetAllStudentsAsync();
                return Ok(studentsList);
            }
            catch(Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("get-student-by/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Student))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<IActionResult> GetStudentById(int id)
        {
            try
            {
                var student = await _studentService.GetStudentByIdAsync(id);
                return Ok(student);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);

            }
        }

        [HttpGet("get-student-address-by/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AddressDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<IActionResult> GetStudentByAddress(int id)
        {
            try
            {
                var address = await _studentService.GetStudentByAdressAsync(id);
                return Ok(address);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);

            }
        }

        [HttpPost("create-student-without-address")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Student))]
        public async Task<IActionResult> CreateStudentWithoutAdress([FromBody] StudentWithoutAddressDto request)
        {
            var student = await _studentService.CreateStudentWithoutAdressAsync(request);
            return CreatedAtAction(nameof(GetStudentById), new { id = student.Id }, student);
        }

        [HttpPut("update-student/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Student))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<IActionResult> UpdateStudent(int id,[FromBody] StudentWithoutAddressDto request)
        {
            try
            {
                var student = await _studentService.UpdateStudentAsync(id, request);
                return Ok(student);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);

            }
        }

        [HttpPut("update-student-address/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(StudentWithAddress))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<IActionResult> UpdateStudentAddress(int id, [FromBody] AddressDto request)
        {
            try
            {
                var result = await _studentService.UpdateStudentAddressAsync(id, request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message); 

            }
        }

        [HttpDelete("delete-student-and-address/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Student))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<ActionResult<Student>> DeleteStudent(int studentId,[FromQuery] bool deleteAddress = false)
        {
            try
            {
                await _studentService.DeleteStudentAsync(studentId, deleteAddress);
                return Ok(new {message = $"Student with id {studentId} was successfully deleted!"});
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);

            }
        }
    }
}
