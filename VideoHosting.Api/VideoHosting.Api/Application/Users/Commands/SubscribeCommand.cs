using Hellang.Middleware.ProblemDetails;
using MediatR;
using VideoHosting.Api.Common;
using VideoHosting.Common.Responses;
using VideoHosting.Database.Abstraction;

namespace VideoHosting.Api.Application.Users.Commands;

public class SubscribeCommand : IRequest<Response<Unit>>
{
    public string LoggedInUserId { get; }
    public string UserToSubscribe { get; }

    public SubscribeCommand(string loggedInUserId, string userToSubscribe)
    {
        LoggedInUserId = loggedInUserId;
        UserToSubscribe = userToSubscribe;
    }

    public class Handler : BaseHandler<SubscribeCommand, Unit>
    {
        public Handler(IUnitOfWork unit) : base(unit)
        {
        }

        public override async Task<Response<Unit>> Handle(SubscribeCommand request, CancellationToken cancellationToken)
        {
            var subscriber = await Unit.UserRepository.GetUserById(request.LoggedInUserId);
            if (subscriber == null)
            {
                throw new ProblemDetailsException(StatusCodes.Status404NotFound, "Logged in user not found");
            }
            var subscripter = await Unit.UserRepository.GetUserById(request.UserToSubscribe);
            if (subscripter == null)
            {
                throw new ProblemDetailsException(StatusCodes.Status404NotFound, "Subscription user not found");
            }

            if (subscriber.Subscriptions.FirstOrDefault(x => x.Subscripter == subscripter) == null)
            {
                subscriber.Subscribe(subscripter);
            }
            else
            {
                subscriber.Unsubscribe(subscripter);
            }

            return Success(new Unit());
        }
    }
}
