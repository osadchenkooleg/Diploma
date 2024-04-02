using MediatR;
using VideoHosting_Back_end.Application.Credentials.Models;
using VideoHosting_Back_end.Common.Common;
using VideoHosting_Back_end.Database.Abstraction;

namespace VideoHosting_Back_end.Application.Credentials.Commands;

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
