using Authentication.Domain.Commands;
using Authentication.Domain.Services.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Authentication.Api.Controllers
{
    [Route("api/users")]
    [Authorize]
    [ApiController]
    public class UserController : ControllerBase
    {
        // Método 1
        [HttpPost]
        [Route("token")]
        [AllowAnonymous]
        public async Task<IActionResult> ValidateUser(
            [FromBody] ValidateUserCommand command,
            [FromServices] IMediator mediator)
        {
            return Ok(await mediator.Send(command));
        }

        // Método 2
        [HttpPost]
        [Route("security")]
        public async Task<IActionResult> ValidatePassword(
            [FromBody] ValidatePasswordCommand command,
            [FromServices] IMediator mediator)
        {
            return Ok(await mediator.Send(command));
        }

        // Método 3
        [HttpGet]
        [Route("")]
        public IActionResult GeneratePassword(
            [FromServices] IUserService userService)
        {
            return Ok(userService.GeneratePassword());
        }

    }
}
