using Hellang.Middleware.ProblemDetails;
using MediatR;
using VideoHosting_Back_end.Application.Users.Helper;
using VideoHosting_Back_end.Application.Users.Models;
using VideoHosting_Back_end.Common.Common;
using VideoHosting_Back_end.Database.Abstraction;

namespace VideoHosting_Back_end.Application.Users.Queries;

public class GetSubscribersQuery : IRequest<Response<IEnumerable<UserModel>>>
{
    public string UserId { get; }

    public GetSubscribersQuery(string userId)
    {
        UserId = userId;
    }

    public class Handler : BaseHandler<GetSubscribersQuery, IEnumerable<UserModel>>
    {
        public Handler(IUnitOfWork unit) : base(unit)
        {
        }

        public override async Task<Response<IEnumerable<UserModel>>> Handle(GetSubscribersQuery request, CancellationToken cancellationToken)
        {
            var user = await Unit.UserRepository.GetUserById(request.UserId);
            if (user == null)
            {
                throw new ProblemDetailsException(StatusCodes.Status404NotFound, "Requested user not found");
            }

            var userModels = new List<UserModel>();

            foreach (var subscriber in user.Subscribers.Select(x => x.Subscripter))
            {
                userModels.Add(await UserHelper.GetUserByIdAsync(subscriber.Id, request.UserId,  Unit));
            }
            
            return Success(userModels);
        }
    }
}