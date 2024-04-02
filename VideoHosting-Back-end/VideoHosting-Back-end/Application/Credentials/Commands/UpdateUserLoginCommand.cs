using Hellang.Middleware.ProblemDetails;
using MediatR;
using VideoHosting_Back_end.Application.Credentials.Models;
using VideoHosting_Back_end.Common.Common;
using VideoHosting_Back_end.Database.Abstraction;

namespace VideoHosting_Back_end.Application.Credentials.Commands;

public class UpdateUserLoginCommand : IRequest<Response<Unit>>
{
    public string LoggedInUserId { get; }
    public LoginUserModel LoginUserModel { get; }

    public UpdateUserLoginCommand(string loggedInUserId, LoginUserModel loginUserModel)
    {
        LoggedInUserId = loggedInUserId;
        LoginUserModel = loginUserModel;
    }
    public class Handler : BaseHandler<UpdateUserLoginCommand, Unit>
    {
        public Handler(IUnitOfWork unit) : base(unit)
        {
        }

        public override async Task<Response<Unit>> Handle(UpdateUserLoginCommand request, CancellationToken cancellationToken)
        {
            var user = await Unit.UserManager.FindByIdAsync(request.LoggedInUserId);
            if (user == null)
            {
                throw new ProblemDetailsException(StatusCodes.Status404NotFound, "User not found");
            }

            user.Email = request.LoginUserModel.Email;

            await Unit.SaveAsync();

            return Success(new Unit());
        }
    }
}
