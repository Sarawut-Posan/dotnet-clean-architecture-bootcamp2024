using Application.Features.Register.Commands.CreateAccount;
using Application.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator mediator;

        public AuthController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult> Register(RegisterRequestDto registerModel)
        {
            var result = await mediator.Send(new CreateAccountCommand { RegisterModel = registerModel });
            if (result == false)
            {
                return BadRequest();
            }

            return Ok(result);
        }
       
    }
}
