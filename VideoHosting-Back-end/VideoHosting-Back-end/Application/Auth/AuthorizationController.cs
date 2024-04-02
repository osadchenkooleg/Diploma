using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using VideoHosting_Back_end.Application.Auth.Commands;
using VideoHosting_Back_end.Application.Auth.Models;

namespace VideoHosting_Back_end.Application.Auth;

[Route("api")]
[ApiController]
public class AuthorizationController : BaseController
{
    private readonly IConfiguration _configuration;

    public AuthorizationController(IMediator mediator, IConfiguration configuration) : base(mediator)
    {
        _configuration = configuration;
    }

    [HttpPost]
    [Route("Register")]
    public async Task<IActionResult> Register(UserRegistrationModel model)
    {
        if (ModelState.IsValid)
        {
            var result = await Mediator.Send(new CreateUserCommand(model), HttpContext.RequestAborted);
            return Result(result);
        }

        return BadRequest("Invalid data," + ModelState.Values);
    }

    [HttpPost]
    [Route("Login")]
    public async Task<IActionResult> Login(UserEnterModel model)
    {
        if (ModelState.IsValid)
        {
            var result = await Mediator.Send(new LoginCommand(model), HttpContext.RequestAborted);
            return Result(result);
        }
        return BadRequest("Invalid login");
    }

    
    [HttpPost]
    [Route("Logout")]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync("Bearer");
        return Ok();
    }

}
