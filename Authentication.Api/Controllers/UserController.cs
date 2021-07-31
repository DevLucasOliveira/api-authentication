using Authentication.Domain.Commands;
using Authentication.Domain.Services.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult ValidateUser(
            [FromBody] ValidateUserCommand command,
            [FromServices] IMediator mediator)
        {
            return Ok(mediator.Send(command));
        }

        // Método 2
        [HttpPost]
        [Route("security")]
        public IActionResult ValidatePassword(
            [FromBody] ValidateUserCommand command,
            [FromServices] IMediator mediator)
        {
            return Ok(mediator.Send(command));
        }

        // Método 3
        [HttpPost]
        [Route("")]
        public IActionResult GeneratePassword(
            [FromServices] IUserService userService)
        {
            return Ok(userService.GeneratePassword());
        }

    }
}
