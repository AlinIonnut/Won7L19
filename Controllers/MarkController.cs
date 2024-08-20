using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Won7E1.Data;
using Won7E1.DTOs;
using Won7E1.Models;
using Won7E1.Service;

namespace Won7E1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MarkController : ControllerBase
    {
        private readonly MarkService _markService;

        public MarkController(MarkService markService)
        {
            _markService = markService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MarkDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        public IActionResult CreateMark(MarkDto request)
        {
            try
            {
                var mark = _markService.CreateMark(request);
                return Ok(mark);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }

        }

        [HttpGet("student/marks/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<MarksForStudent>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        public IActionResult GetAllMarksForStudent(int id)
        {
            try
            {
                var marks = _markService.GetAllMarksForStudent(id);
                return Ok(marks);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("{studentId}/marks/{subjectId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<MarksFromASubject>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        public IActionResult GetAllMarksFromASubject(int studentId, int subjectId)
        {
            try
            {
                var marks = _markService.GetAllMarksFromASubject(studentId, subjectId);
                return Ok(marks);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("{studentId}/subject-average")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<SubjectAverageDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        public IActionResult GetSubjectAverage(int studentId)
        {
            try
            {
                var subjectAverage = _markService.GetSubjectAverage(studentId);
                return Ok(subjectAverage);
            }
            catch(Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("sort-by-grades")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<StudentsOrderByGrades>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        public IActionResult GetStudentsByGrades([FromQuery] string order = "asc")
        {
            try
            {
                var listOfStudents = _markService.GetStudentsByGrades(order);
                return Ok(listOfStudents);
            }
            catch(Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

    }
}
