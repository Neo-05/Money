using Microsoft.AspNetCore.Mvc;
using Money.BLL.Interfaces;
using Money.BLL.Exceptions;
using MoneyApi.DTOs;
using MoneyApi.Mappers;

namespace MoneyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpenseController : ControllerBase
    {
        readonly private IExpenseService _ExpenseService;
        public ExpenseController(IExpenseService expenseService)
        {
            _ExpenseService = expenseService;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ExpenseDTO>))]
        public IActionResult GetAll()
        {
            IEnumerable<ExpenseDTO> result = _ExpenseService.GetAll().Select(e => e.ToDTO());
            return Ok(result);
        }



        [HttpGet("{expenseId}")]
        [ProducesResponseType(404, Type = typeof(string))]
        [ProducesResponseType(200, Type = typeof(ExpenseDTO))]
        public IActionResult GetById([FromRoute] int expenseId)
        {
            ExpenseDTO? expenseDTO = _ExpenseService.GetById(expenseId)?.ToDTO();
            if (expenseDTO is null)
            {
                return NotFound("Expense not found");
            }
            return Ok(expenseDTO);
        }

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(ExpenseDTO))]
        public IActionResult Create([FromBody] ExpenseDataDTO expense)
        {
            ExpenseDTO result = _ExpenseService.Create(expense.ToModel()).ToDTO();
            return CreatedAtAction(nameof(GetById), new { expenseId = result.Id_Expense }, result);
        }



        [HttpDelete("{expenseId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404, Type = typeof(string))]
        public IActionResult Delete([FromRoute] int expenseId)
        {
            bool deleted = _ExpenseService.Delete(expenseId);

            if (deleted)
            {
                return Ok();
            }
            else
            {
                return NotFound("Expense not found");
            }
        }

        [HttpPut("{expenseId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404, Type = typeof(string))]
        public IActionResult Update([FromRoute] int expenseId, [FromBody] ExpenseDataDTO expense)
        {
            bool updated;
            try
            {
                updated = _ExpenseService.Update(expenseId, expense.ToModel());
            }
            catch (NotFoundException nFE)
            {
                return NotFound(nFE.Message);
            }

            return updated ? NoContent() : NotFound();
        }


        [HttpGet("expense/filters")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ExpenseDTO>))]
        [ProducesResponseType(400, Type = typeof(string))]

        public IActionResult GetFilteredExpense([FromQuery]DateTime startDate, [FromQuery] DateTime endDate, [FromQuery] List<int> categoryIds)
        {
            try
            {
                IEnumerable<ExpenseDTO> result = _ExpenseService.GetFilteredExpenses(startDate, endDate, categoryIds).Select(e => e.ToDTO());
                if (result != null)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest("Echec");
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message}");
            }
        }
    }
}
