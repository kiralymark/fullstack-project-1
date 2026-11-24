using System.Text;
using fullstack_project_1.Data;
using fullstack_project_1.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace fullstack_project_1.Controllers
{
    // (api/subjects/)
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectsController : ControllerBase
    {
        
        public SubjectsService _subjectsService;

        public SubjectsController(SubjectsService subjectsService)
        {
            _subjectsService = subjectsService;
        }

        /// <summary>
        /// Get all subjects.
        /// </summary>
        /// <remarks>
        /// Get all subjects with their information from the database.
        /// </remarks>
        /// <response code="200">Get subjects was successfull.</response>
        //[Authorize(Roles = UserRoles.Author)]
        [HttpGet("get-all-subjects")]
        [SwaggerOperation(
            Summary = "Get all subjects",
            Description = "Get all subjects, with info",
            OperationId = "GetAllSubjects"
        )]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetAllSubjects()
        {
            var allSubjects = _subjectsService.GetAllSubjects();
            return Ok(allSubjects);
        }

        //[Authorize(Roles = UserRoles.Admin)]
        [HttpGet("get-subject-by-id/{id}")]
        public IActionResult GetSubjectById(int id)
        {
            var subject = _subjectsService.GetSubjectById(id);
            return Ok(subject);
        }

    }

}
