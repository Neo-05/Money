using Microsoft.AspNetCore.Mvc;
using Money.BLL.Interfaces;
using Money.BLL.Exceptions;
using MoneyApi.DTOs;
using MoneyApi.Mappers;
using Money.BLL.Services;

namespace MoneyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _ProjectService;
        public ProjectController(IProjectService projectService)
        {
            _ProjectService = projectService;
        }


        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ProjectDTO>))]
        public IActionResult GetAll()
        {
            IEnumerable<ProjectDTO> result = _ProjectService.GetAll().Select(p => p.ToDTO());
            return Ok(result);
        }



        [HttpGet("{projectId}")]
        [ProducesResponseType(404, Type = typeof(string))]
        [ProducesResponseType(200, Type = typeof(ProjectDTO))]
        public IActionResult GetById([FromRoute] int projectId)
        {
            ProjectDTO? projectDTO = _ProjectService.GetById(projectId)?.ToDTO();
            if (projectDTO is null)
            {
                return NotFound("Project not found");
            }
            return Ok(projectDTO);
        }

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(ProjectDTO))]
        public IActionResult Create([FromBody] ProjectDataDTO project)
        {
            ProjectDTO result = _ProjectService.Create(project.ToModel()).ToDTO();
            return CreatedAtAction(nameof(GetById), new { ProjectId = result.Id }, result);
        }



        [HttpDelete("{projectId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404, Type = typeof(string))]
        public IActionResult Delete([FromRoute] int projectId)
        {
            bool deleted = _ProjectService.Delete(projectId);

            if (deleted)
            {
                return Ok();
            }
            else
            {
                return NotFound("Project not found");
            }
        }

        [HttpPut("{projectId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404, Type = typeof(string))]
        public IActionResult Update([FromRoute] int projectId, [FromBody] ProjectDataDTO project)
        {
            bool updated;
            try
            {
                updated = _ProjectService.Update(projectId, project.ToModel());
            }
            catch (NotFoundException nFE)
            {
                return NotFound(nFE.Message);
            }
            return updated ? NoContent() : NotFound();
        }
    }
}
