using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Money.BLL.Interfaces;
using Money.BLL.Exceptions;
using MoneyApi.DTOs;
using MoneyApi.Mappers;

namespace MoneyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        private readonly IPeopleService _PeopleService;

        public PeopleController(IPeopleService peopleService)
        {
            _PeopleService = peopleService;
        }


        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<PeopleDTO>))]
        public IActionResult GetAll()
        {
            IEnumerable<PeopleDTO> result = _PeopleService.GetAll().Select(p => p.ToDTO());
            return Ok(result);
        }




        [HttpGet("{peopleId}")]
        [ProducesResponseType(404, Type = typeof(string))]
        [ProducesResponseType(200, Type = typeof(PeopleDTO))]
        public IActionResult GetById([FromRoute] int peopleId)
        {
            PeopleDTO? peopleDTO = _PeopleService.GetById(peopleId)?.ToDTO();
            if (peopleDTO is null)
            {
                return NotFound("Artist not found");
            }
            return Ok(peopleDTO);
        }

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(PeopleDTO))]
        public IActionResult Create([FromBody] PeopleDataDTO people)
        {
            PeopleDTO result = _PeopleService.Create(people.ToModel()).ToDTO();
            return CreatedAtAction(nameof(GetById), new { peopleId = result.Id }, result);
        }



        [HttpDelete("{peopleId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404, Type = typeof(string))]
        public IActionResult Delete([FromRoute] int peopleId)
        {
            bool deleted = _PeopleService.Delete(peopleId);

            if (deleted)
            {
                return Ok();
            }
            else
            {
                return NotFound("People not found");
            }
        }

        [HttpPut("{peopleId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404, Type = typeof(string))]
        public IActionResult Update([FromRoute] int peopleId, [FromBody] PeopleDataDTO people)
        {
            bool updated;
            try
            {
                updated = _PeopleService.Update(peopleId, people.ToModel());
            }
            catch (NotFoundException nFE)
            {
                return NotFound(nFE.Message);
            }

            return updated ? NoContent() : NotFound();
        }
    }
}
