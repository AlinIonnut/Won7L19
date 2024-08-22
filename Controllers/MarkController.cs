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
        public async Task<IActionResult> CreateMark(MarkDto request)
        {
            try
            {
                var mark = await _markService.CreateMarkAsync(request);
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
        public async Task<IActionResult> GetAllMarksForStudent(int id)
        {
            try
            {
                var marks = await _markService.GetAllMarksForStudentAsync(id);
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
        public async Task<IActionResult> GetAllMarksFromASubject(int studentId, int subjectId)
        {
            try
            {
                var marks = await _markService.GetAllMarksFromASubjectAsync(studentId, subjectId);
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
        public async Task<IActionResult> GetSubjectAverage(int studentId)
        {
            try
            {
                var subjectAverage = await _markService.GetSubjectAverageAsync(studentId);
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
        public async Task<IActionResult> GetStudentsByGrades([FromQuery] string order = "asc")
        {
            try
            {
                var listOfStudents = await _markService.GetStudentsByGradesAsync(order);
                return Ok(listOfStudents);
            }
            catch(Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

    }
}
