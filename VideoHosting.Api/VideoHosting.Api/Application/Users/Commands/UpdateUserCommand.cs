using Hellang.Middleware.ProblemDetails;
using MediatR;
using VideoHosting.Api.Common;
using VideoHosting.Common.Responses;
using VideoHosting.Database.Abstraction;
using VideoHosting_Back_end.Application.Users.Models;

namespace VideoHosting.Api.Application.Users.Commands;

public class UpdateUserCommand : IRequest<Response<Unit>>
{
    public string UserId { get; }
    public UserUpdateModel UpdateModel { get; }

    public UpdateUserCommand(string userId, UserUpdateModel updateModel)
    {
        UserId = userId;
        UpdateModel = updateModel;
    }

    public class Handler : BaseHandler<UpdateUserCommand, Unit>
    {
        public Handler(IUnitOfWork unit) : base(unit)
        {
        }

        public override async Task<Response<Unit>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await Unit.UserRepository.GetUserById(request.UserId);
            if (user == null)
            {
                throw new ProblemDetailsException(StatusCodes.Status404NotFound, "Logged in user not found");
            }


            user.Name = request.UpdateModel.Name ?? user.Name;
            user.Surname = request.UpdateModel.Surname ?? user.Surname;
            user.Group = request.UpdateModel.Group ?? user.Group;
            user.Faculty = request.UpdateModel.Faculty ?? user.Faculty;
            user.Sex = request.UpdateModel.Sex ?? user.Sex;

            await Unit.SaveAsync();

            return Success(new Unit());
        }
    }
}
