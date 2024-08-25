using Application.Features.Category.Commands.CreateCategory;
using Application.Features.Category.Commands.DeleteCategory;
using Application.Features.Category.Commands.UpdateCategory;
using Application.Features.Category.Queries.GetAllCategories;
using Application.Features.Category.Queries.GetCategoryById;
using Application.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase {

        private readonly IMediator mediator;

        public CategoryController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategories() {
            var categories = await mediator.Send(new GetAllCategoriesQuery());

            return Ok(categories);
        }


        [HttpGet ("{id:guid}")]
        public async Task<IActionResult> GetCategoryById([FromRoute] Guid id)
        {
            var query = new GetCategoryByIdQuery { Id = id };
            var result = await mediator.Send(query);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody]  CreateCategoryRequestDto request)
        {
           var command = new CreateCategoryCommand { Request = request };
           var result = await mediator.Send(command);
            if (result == null)
            {
                return BadRequest();
            }
           return Ok(result);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateCategory([FromRoute] Guid id, [FromBody] UpdateCategoryRequestDto request)
        {
            var command = new UpdateCategoryCommand { Request = request , Id = id };
            var result = await mediator.Send(command);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteCategory([FromRoute] Guid id)
        {
            var command = new DeleteCategoryCommand { Id = id };
            var result = await mediator.Send(command);

            if (result == null)
            {
                return NotFound();
            }
          
            return Ok(result);
        }
    }
}
