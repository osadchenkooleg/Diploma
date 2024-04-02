using MediatR;
using Microsoft.AspNetCore.Mvc;
using VideoHosting_Back_end.Common.Common;

namespace VideoHosting_Back_end.Application;

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
            return ValidationProblem(ModelState);
        }
    }
}
