using MediatR;
using VideoHosting_Back_end.Application.Users.Helper;
using VideoHosting_Back_end.Application.Users.Models;
using VideoHosting_Back_end.Common.Common;
using VideoHosting_Back_end.Database.Abstraction;

namespace VideoHosting_Back_end.Application.Users.Queries;

public class GetUserQuery : IRequest<Response<UserModel>>
{
    public string UserId { get; }
    public string LoggedInUser { get; }

    public GetUserQuery(string userId, string loggedInUser)
    {
        UserId = userId;
        LoggedInUser = loggedInUser;
    }

    public class Handler : BaseHandler<GetUserQuery, UserModel>
    {
        public Handler(IUnitOfWork unit) : base(unit)
        {
        }

        public override async Task<Response<UserModel>> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            var userModel = await UserHelper.GetUserByIdAsync(request.UserId, request.LoggedInUser, Unit);
            
            return Success(userModel);
        }
    }
}