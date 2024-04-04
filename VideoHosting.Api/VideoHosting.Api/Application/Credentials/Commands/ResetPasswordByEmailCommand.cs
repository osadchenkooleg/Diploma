using Hellang.Middleware.ProblemDetails;
using MediatR;
using VideoHosting.Api.Application.Credentials.Models;
using VideoHosting.Api.Common;
using VideoHosting.Common.Responses;
using VideoHosting.Database.Abstraction;

namespace VideoHosting.Api.Application.Credentials.Commands;

public class ResetPasswordByEmailCommand : IRequest<Response<Unit>>
{
    public ResetPasswordModelByEmail Model { get; }

    public ResetPasswordByEmailCommand(ResetPasswordModelByEmail model)
    {
        Model = model;
    }
    public class Handler : BaseHandler<ResetPasswordByEmailCommand, Unit>
    {
        public Handler(IUnitOfWork unit) : base(unit)
        {
        }

        public override async Task<Response<Unit>> Handle(ResetPasswordByEmailCommand request, CancellationToken cancellationToken)
        {
            var model = request.Model;

            var user = await Unit.UserManager.FindByEmailAsync(model.Email);
            if (user.TempPassword == model.TempPassword && user.TempPassword != null)
            {
                user.TempPassword = null;
                await Unit.UserManager.RemovePasswordAsync(user);
                await Unit.UserManager.AddPasswordAsync(user, model.Password);
                await Unit.SaveAsync();
            }
            else
            {
                throw new ProblemDetailsException(StatusCodes.Status400BadRequest, "Invalid password or login");
            }

            return Success(new Unit());
        }
    }
}
