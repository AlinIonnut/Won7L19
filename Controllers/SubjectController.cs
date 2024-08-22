using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Won7E1.DTOs;
using Won7E1.Models;
using Won7E1.Service;

namespace Won7E1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectController : ControllerBase
    {
        private readonly SubjectService _subjectService;
        public SubjectController(SubjectService subjectService)
        {
            _subjectService = subjectService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SubjectDto))]
        public async Task<IActionResult> CreateSubject([FromBody]SubjectDto request)
        {
            var subject = await _subjectService.CreateSubjectAsync(request);
            return Ok(subject);
        }
    }
}
