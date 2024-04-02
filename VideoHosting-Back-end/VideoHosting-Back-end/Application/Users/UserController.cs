using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using VideoHosting_Back_end.Application.Credentials.Models;
using VideoHosting_Back_end.Application.Users.Commands;
using VideoHosting_Back_end.Application.Users.Models;
using VideoHosting_Back_end.Application.Users.Queries;

namespace VideoHosting_Back_end.Application.Users;
[Route("api/[controller]")]
[ApiController]
public class UserController : BaseController
{
    public UserController(IMediator mediator) : base(mediator)
    {
    }

    [HttpPost]
    [Route("exist")]
    public async Task<IActionResult> IsExist(LoginUserModel model)
    {
        if (ModelState.IsValid)
        {
            return BadRequest("Invalid data");
        }
        var result = await Mediator.Send(new IsExistQuery(model), HttpContext.RequestAborted);
        return Result(result);
    }

    [HttpPost]
    [Route("addPhoto")]
    public async Task<IActionResult> UploadPhoto()
    {
        var files = HttpContext.Request.Form.Files;
        var result = await Mediator.Send(new UploadPhotoCommand(files, User.Identity?.Name ?? string.Empty), HttpContext.RequestAborted);
        return Result(result);
    }

    [HttpPut]
    [Route("subscribe/{Id}")]
    public async Task<IActionResult> Subscribe(string id)
    {
        var result = await Mediator.Send(new SubscribeCommand(User.Identity?.Name ?? string.Empty, id), HttpContext.RequestAborted);
        return Result(result);
    }
    
    [HttpPut]
    [Route("updateUser")]
    public async Task<IActionResult> UpdateUser(UserUpdateModel model)
    {
        var result = await Mediator.Send(new UpdateUserCommand(User.Identity?.Name ?? string.Empty, model), HttpContext.RequestAborted);
        return Result(result);
    }


    [HttpGet]
    [Route("profileUser/{Id}")]
    public async Task<IActionResult> GetUser(string id)
    {
        var result = await Mediator.Send(new GetUserQuery(id, User.Identity?.Name ?? string.Empty), HttpContext.RequestAborted);
        return Result(result);
    }

    [HttpGet]
    [Route("subscribers")]
    public async Task<IActionResult> GetSubscribers()
    {
        var result = await Mediator.Send(new GetSubscribersQuery(User.Identity?.Name ?? string.Empty), HttpContext.RequestAborted);
        return Result(result);
    }

    [HttpGet]
    [Route("subscriptions")]
    public async Task<IActionResult> GetSubscriptions()
    {
        var result = await Mediator.Send(new GetSubscriptionsQuery(User.Identity?.Name ?? string.Empty), HttpContext.RequestAborted);
        return Result(result);
    }

    [HttpGet]
    [Route("findByName/{userName}")]
    public async Task<IActionResult> GetUserByName(string userName)
    {
        var result = await Mediator.Send(new GetUserByNameQuery(userName, User.Identity?.Name ?? string.Empty), HttpContext.RequestAborted);
        return Result(result);
    }
}
