using MediatR;
using VideoHosting_Back_end.Application.Users.Helper;
using VideoHosting_Back_end.Application.Users.Models;
using VideoHosting_Back_end.Common.Common;
using VideoHosting_Back_end.Database.Abstraction;

namespace VideoHosting_Back_end.Application.Users.Queries;

public class GetUserByNameQuery : IRequest<Response<IEnumerable<UserModel>>>
{
    public string UserName { get; }
    public string LoggedInUser { get; }

    public GetUserByNameQuery(string userName, string loggedInUser)
    {
        UserName = userName;
        LoggedInUser = loggedInUser;
    }

    public class Handler : BaseHandler<GetUserByNameQuery, IEnumerable<UserModel>>
    {
        public Handler(IUnitOfWork unit) : base(unit)
        {
        }

        public override async Task<Response<IEnumerable<UserModel>>> Handle(GetUserByNameQuery request, CancellationToken cancellationToken)
        {
            var users = await Unit.UserRepository.GetUserBySubName(request.UserName);
            var userModels = new List<UserModel>();

            foreach (var u in users)
            {
                userModels.Add(await UserHelper.GetUserByIdAsync(u.Id, request.LoggedInUser,  Unit));
            }
            
            return Success(userModels);
        }
    }
}