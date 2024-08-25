using Application.Features.BlogPost.Commands;
using Application.Features.BlogPost.Commands.CreateBlogPost;
using Application.Features.BlogPost.Commands.UpdateBlogPost;
using Application.Features.BlogPost.Queries.GetAllBlogPost;
using Application.Features.BlogPost.Queries.GetBlogPostById;
using Application.Features.Category.Queries.GetAllCategories;
using Application.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogPostController : ControllerBase
    {
        private readonly IMediator mediator;

        public BlogPostController(IMediator mediator)
        {
            this.mediator = mediator;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllBlogPost()
        {
            var blogPosts = await mediator.Send(new GetAllBlogPostQuery()); 

            return Ok(blogPosts);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetBlogPostById([FromRoute] Guid id)
        {
            var query = new GetBlogPostByIdQuery { Id = id };
            var result = await mediator.Send(query);
            if (result == null)
            {
                return NotFound();
            }
            
            return Ok(result);
        }


        [HttpPost]
        public async Task<IActionResult> CreateBlogPost([FromBody] CreateBlogPostRequestDto request)
        {
            var command = new CreateBlogPostCommand { Request = request };
            var result = await mediator.Send(command);
            if (result == null)
            {
                return BadRequest();
            }

            return Ok(result);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateBlogPost([FromRoute] Guid id, [FromBody] UpdateBlogPostRequestDto request)
        {
            var command = new UpdateBlogPostCommand { Id = id };
            var result = await mediator.Send(command);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }
       
    }
}