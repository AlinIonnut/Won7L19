using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Won7E1.Data;
using Won7E1.DTOs;
using Won7E1.Models;

namespace Won7E1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly DataContext _dbContext;

        public StudentController(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public ActionResult<List<Student>> GetAllStudents()
        {
            var students = _dbContext.Students.ToList();
            if (students == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(students);
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Student> GetStudentById(int id)
        {
            var student = _dbContext.Students.FirstOrDefault(s => s.Id == id);
            if (student == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(student);
            }
        }

        [HttpGet("{id}/address")]
        public ActionResult<Student> GetStudentByAddress(int id)
        {
            var student = _dbContext.Students.Include(s => s.Address).FirstOrDefault(s => s.Id == id);
            if (student == null)
            {
                return NotFound();
            }

            if (student.Address == null)
            {
                return NotFound();
            }

            var addressDto = new AddressDto
            {
                City = student.Address.City,
                Street = student.Address.Street,
                Number = student.Address.Number,
            };

            return Ok(addressDto);
        }

        [HttpPost]
        public ActionResult<Student> CreateStudentWithoutAdress([FromBody] StudentWithoutAddressDto request)
        {
            var newStudent = new Student
            {
                Name = request.Name,
                FirstName = request.FirstName,
                Age = request.Age,
            };

            _dbContext.Students.Add(newStudent);
            _dbContext.SaveChanges();

            return Ok(newStudent);
        }

        [HttpPut("{id}")]
        public ActionResult<Student> UpdateStudent(int id,[FromBody] StudentWithoutAddressDto request)
        {
            var student = _dbContext.Students.FirstOrDefault( s => s.Id == id);
            if (student == null)
            {
                return NotFound();
            }

            student.Name = request.Name;
            student.FirstName = request.FirstName;
            student.Age = request.Age;

            _dbContext.SaveChanges();

            return Ok(student);
        }

        [HttpPut("{id}/address")]
        public ActionResult<Student> UpdateStudentAddress(int id,[FromBody] AddressDto request)
        {
            var student = _dbContext.Students.Include( s => s.Address).FirstOrDefault(s => s.Id == id);
            if (student == null)
            {
                return NotFound();
            }

            if(student.Address == null)
            {
                var newAddress = new Address
                {
                    City = request.City,
                    Street = request.Street,
                    Number = request.Number,
                };

                _dbContext.Addresses.Add(newAddress);
                student.Address = newAddress;
            }
            else
            {
                student.Address.Street = request.Street;
                student.Address.City = request.City;
                student.Address.Number = request.Number;
            }

            _dbContext.SaveChanges();

            var fullInformationsOfStudent = new StudentWithAddress
            {
                Name = student.Name,
                FirstName = student.FirstName,
                Address = student.Address == null ? null : new AddressDto
                {
                    City = student.Address.City,
                    Street = student.Address.Street,
                    Number = student.Address.Number,
                }
            };

            return Ok(fullInformationsOfStudent);
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteStudent(int id,[FromQuery] bool deleteAddress = false)
        {
            var student = _dbContext.Students.Include(s => s.Address).FirstOrDefault(s => s.Id == id);
            if (student == null)
            {
                return NotFound();
            }

            if (deleteAddress && student.Address != null)
            {
                _dbContext.Addresses.Remove(student.Address);
            }

            _dbContext.Students.Remove(student);
            _dbContext.SaveChanges();

            return Ok(student);
        }


        [HttpDelete("{id}/address")]
        public ActionResult DeleteStudentAddress(int id)
        {

            var student = _dbContext.Students.Include(s => s.Address).FirstOrDefault(s => s.Id == id);
            if (student == null)
            {
                return NotFound();
            }

            if (student.Address == null)
            {
                return NotFound("Address not found for this student.");
            }

            _dbContext.Addresses.Remove(student.Address);
            _dbContext.SaveChanges();

            return Ok(student);
        }
    }
}
