using Microsoft.AspNetCore.Mvc;
using Money.BLL.Interfaces;
using Money.BLL.Exceptions;
using MoneyApi.DTOs;
using MoneyApi.Mappers;

namespace MoneyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IncomeController : ControllerBase
    {
        private readonly IIncomeService _IncomeService;

        public IncomeController(IIncomeService incomeService)
        {
            _IncomeService = incomeService;
        }



        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<IncomeDTO>))]
        public IActionResult GetAll()
        {
            IEnumerable<IncomeDTO> result = _IncomeService.GetAll().Select(i => i.ToDTO());
            return Ok(result);
        }



        [HttpGet("{incomeId}")]
        [ProducesResponseType(404, Type = typeof(string))]
        [ProducesResponseType(200, Type = typeof(IncomeDTO))]
        public IActionResult GetById([FromRoute] int incomeId)
        {
            IncomeDTO? incomeDTO = _IncomeService.GetById(incomeId)?.ToDTO();
            if (incomeDTO is null)
            {
                return NotFound("Income not found");
            }
            return Ok(incomeDTO);
        }

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(IncomeDTO))]
        public IActionResult Create([FromBody] IncomeDataDTO income)
        {
            IncomeDTO result = _IncomeService.Create(income.ToModel()).ToDTO();
            return CreatedAtAction(nameof(GetById), new { incomeId = result.Id }, result);
        }



        [HttpDelete("{incomeId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404, Type = typeof(string))]
        public IActionResult Delete([FromRoute] int incomeId)
        {
            bool deleted = _IncomeService.Delete(incomeId);

            if (deleted)
            {
                return Ok();
            }
            else
            {
                return NotFound("Income not found");
            }
        }

        [HttpPut("{incomeId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404, Type = typeof(string))]
        public IActionResult Update([FromRoute] int incomeId, [FromBody] IncomeDataDTO income)
        {
            bool updated;
            try
            {
                updated = _IncomeService.Update(incomeId, income.ToModel());
            }
            catch (NotFoundException nFE)
            {
                return NotFound(nFE.Message);
            }
            return updated ? NoContent() : NotFound();
        }
    }
}
