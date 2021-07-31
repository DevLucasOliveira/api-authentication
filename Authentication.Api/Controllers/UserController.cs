using Authentication.Domain.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Authentication.Api.Controllers
{
    [Route("api/users")]
    [Authorize]
    [ApiController]
    public class UserController : ControllerBase
    {

        [HttpPost]
        [Route("token")]
        [AllowAnonymous]
        public async Task<IActionResult> ValidateUser(
            [FromBody] ValidateUserCommand command,
            [FromServices] IMediator mediator)
        {
            return Ok(mediator.Send(command));
        }


    }
}
