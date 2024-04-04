using MediatR;
using Microsoft.AspNetCore.Mvc;
using VideoHosting.Common.Responses;

namespace VideoHosting.Api.Common;

public class BaseController : Controller
{
    public BaseController(IMediator mediator)
    {
        Mediator = mediator;
    }

    public IMediator Mediator { get; set; }

    protected IActionResult Result<T>(Response<T> result)
    {
        if (result.IsSuccess)
        {
            if (result.Result is Unit)
            {
                return NoContent();
            }

            return Ok(result.Result);
        }
        else
        {
            //result.ValidationResult.AddToModelState(ModelState, null);
            return ValidationProblem(ModelState);
        }
    }
}
