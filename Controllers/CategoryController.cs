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
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _CategoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _CategoryService = categoryService;
        }


        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<CategoryDTO>))]
        public IActionResult GetAll()
        {
            IEnumerable<CategoryDTO> result = _CategoryService.GetAll().Select(c => c.ToDTO());
            return Ok(result);
        }



        [HttpGet("{categoryId}")]
        [ProducesResponseType(404, Type = typeof(string))]
        [ProducesResponseType(200, Type = typeof(CategoryDTO))]
        public IActionResult GetById([FromRoute] int categoryId)
        {
            CategoryDTO? categoryDTO = _CategoryService.GetById(categoryId)?.ToDTO();
            if (categoryDTO is null)
            {
                return NotFound("Category not found");
            }
            return Ok(categoryDTO);
        }

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(PeopleDTO))]
        public IActionResult Create([FromBody] CategoryDataDTO category)
        {
            CategoryDTO result = _CategoryService.Create(category.ToModel()).ToDTO();
            return CreatedAtAction(nameof(GetById), new { categoryId = result.Id_Category }, result);
        }



        [HttpDelete("{categoryId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404, Type = typeof(string))]
        public IActionResult Delete([FromRoute] int categoryId)
        {
            bool deleted = _CategoryService.Delete(categoryId);

            if (deleted)
            {
                return Ok();
            }
            else
            {
                return NotFound("Category not found");
            }
        }

        [HttpPut("{categoryId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404, Type = typeof(string))]
        public IActionResult Update([FromRoute] int categoryId, [FromBody] CategoryDataDTO category)
        {
            bool updated;
            try
            {
                updated = _CategoryService.Update(categoryId, category.ToModel());
            }
            catch (NotFoundException nFE)
            {
                return NotFound(nFE.Message);
            }

            return updated ? NoContent() : NotFound();
        }
    }
}
