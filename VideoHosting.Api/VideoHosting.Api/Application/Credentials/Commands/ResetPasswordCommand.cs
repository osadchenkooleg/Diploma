using MediatR;
using VideoHosting.Api.Application.Credentials.Models;
using VideoHosting.Api.Common;
using VideoHosting.Common.Responses;
using VideoHosting.Database.Abstraction;

namespace VideoHosting.Api.Application.Credentials.Commands;

public class ResetPasswordCommand : IRequest<Response<Unit>>
{
    public ResetPasswordModel Model { get; }

    public ResetPasswordCommand(ResetPasswordModel model)
    {
        Model = model;
    }

    public class Handler : BaseHandler<ResetPasswordCommand, Unit>
    {
        public Handler(IUnitOfWork unit) : base(unit)
        {
        }

        public override async Task<Response<Unit>> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await Unit.UserManager.FindByIdAsync(request.Model.UserId);
            await Unit.UserManager.ChangePasswordAsync(user, request.Model.OldPassword, request.Model.Password);

            return Success(new Unit());
        }
    }
}
