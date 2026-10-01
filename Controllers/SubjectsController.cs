using System.Text;
using fullstack_project_1.Data;
using fullstack_project_1.Services;
using Microsoft.AspNetCore.Mvc;

namespace fullstack_project_1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectsController : ControllerBase
    {
        
        public SubjectsService _subjectsService;

        public SubjectsController(SubjectsService subjectsService)
        {
            _subjectsService = subjectsService;
        }

        //[Authorize(Roles = UserRoles.Author)]
        [HttpGet("get-all-subjects")]
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
