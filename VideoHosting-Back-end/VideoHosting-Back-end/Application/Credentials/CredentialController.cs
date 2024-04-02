using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Net;
using VideoHosting_Back_end.Application.Credentials.Commands;
using VideoHosting_Back_end.Application.Credentials.Models;
using VideoHosting_Back_end.Application.Credentials.Queries;

namespace VideoHosting_Back_end.Application.Credentials;
[Route("api/[controller]")]
[ApiController]
public class CredentialController : BaseController
{
    public CredentialController(IMediator mediator) : base(mediator)
    {
    }

    [HttpPut]
    [Route("ResetPassword")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public async Task<IActionResult> ResetPassword(ResetPasswordModel model)
    {
        if (ModelState.IsValid)
        {
            var result = await Mediator.Send(new ResetPasswordCommand(model), HttpContext.RequestAborted);
            return Result(result);
        }

        return BadRequest("Invalid data");

    }

    [HttpPut]
    [Route("RecoverByEmail")]
    public async Task<IActionResult> ResetPasswordByEmail(ResetPasswordModelByEmail model)
    {
        if (ModelState.IsValid)
        {
            var result = await Mediator.Send(new ResetPasswordByEmailCommand(model), HttpContext.RequestAborted);
            return Result(result);
        }

        return BadRequest("Invalid data");
    }

    [HttpPut]
    [Route("DropByEmail")]
    public async Task<IActionResult> DropPassword(string email)
    {
        var result = await Mediator.Send(new DropPasswordCommand(email), HttpContext.RequestAborted);

        var pass = result.Result;

        var myEmail = "oleg.osadchenko.v@gmail.com";
        var password = "Qwerty12345";

        var msg = new MailMessage();
        var smtpClient = new SmtpClient("smtp.gmail.com", 587);
        var loginInfo = new NetworkCredential(email, password);

        msg.From = new MailAddress(myEmail);
        msg.To.Add(new MailAddress(email));
        msg.Subject = "Recreation password";
        msg.Body = "Here is your temporary password " + pass;
        msg.IsBodyHtml = true;

        smtpClient.EnableSsl = true;
        smtpClient.UseDefaultCredentials = false;
        smtpClient.Credentials = loginInfo;


        smtpClient.Send(msg);
        return Ok("Temporary password is send");
    }

    [HttpPut]
    [Route("addAdmin/{Id}")]
    [Authorize(Roles = "SuperAdmin")]
    public async Task<IActionResult> AddAdmin(string id)
    {
        var result = await Mediator.Send(new CreateAdminCommand(id), HttpContext.RequestAborted);
        return Result(result);
    }

    [HttpPut]
    [Route("addAdmin/{Id}")]
    [Authorize(Roles = "SuperAdmin")]
    public async Task<IActionResult> RemoveAdmin(string id)
    {
        var result = await Mediator.Send(new RemoveAdminCommand(id), HttpContext.RequestAborted);
        return Result(result);
    }

    [HttpPut]
    [Route("updateUserLogin")]
    public async Task<IActionResult> UpdateUserLogin(LoginUserModel model)
    {
        if (ModelState.IsValid)
        {
            var result = await Mediator.Send(new UpdateUserLoginCommand(User.Identity.Name, model), HttpContext.RequestAborted);
            return Result(result);
        }
        return BadRequest("Invalid data");
    }

    [HttpGet]
    [Route("loginUser")]
    public async Task<IActionResult> GetLoginUser()
    {
        var result = await Mediator.Send(new GetLoginUserQuery(User.Identity.Name), HttpContext.RequestAborted);
        return Result(result);
    }
}
