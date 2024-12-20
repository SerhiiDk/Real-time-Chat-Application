using ChatApplication.API.V1.Services.UserService;
using ChatApplication.Shared.V1.Models.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace ChatApplication.API.V1.Controllers;

public class AccountController : BaseApiController
{
    [HttpPost(nameof(Login))]
    public async Task<ActionResult> Login([FromServices] IUserService service, [FromBody]LoginUserModel model, CancellationToken cancellationToken)
    {
        var result = await service.Login(model, cancellationToken);

        if (string.IsNullOrEmpty(result.UserName))
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost(nameof(Register))]
    public async Task<ActionResult> Register([FromServices] IUserService service, [FromBody] CreateUserModel model, CancellationToken cancellationToken)
    {
        var result = await service.CreateUser(model, cancellationToken);

        if (!result)
        {
            return BadRequest();
        }

        return NoContent();
    }
}
