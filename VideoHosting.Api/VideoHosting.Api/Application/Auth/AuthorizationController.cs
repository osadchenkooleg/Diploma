using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Chat.IdentityService.Application.Account.Models;
using VideoHosting.Api.Application.Auth.Commands;
using VideoHosting.Api.Application.Auth.Models;
using VideoHosting.Api.Common;

namespace VideoHosting.Api.Application.Auth;

[ApiController]
[Route("api/account")]
[AllowAnonymous]
public class AuthorizationController : BaseController
{
    public AuthorizationController(IMediator mediator) : base(mediator)
    {
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody, Required] LoginModel request)
    {
        var result = await Mediator.Send(new LoginCommand(request), HttpContext.RequestAborted);
        return Result(result);
    }

    [HttpPost("register")]
    public async Task<IActionResult> CreateUser([FromBody, Required] UserApplyModel request)
    {
        var result = await Mediator.Send(new CreateUserCommand(request), HttpContext.RequestAborted);
        return Result(result);
    }
}
