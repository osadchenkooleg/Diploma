using Hellang.Middleware.ProblemDetails;
using MediatR;
using VideoHosting.Api.Application.Videos.Models;
using VideoHosting.Api.Common;
using VideoHosting.Common.Responses;
using VideoHosting.Database.Abstraction;

namespace VideoHosting.Api.Application.Videos.Queries;

public class GetSubscribersVideosQuery : IRequest<Response<IEnumerable<VideoModel>>>
{
    public string LoggedInUserId { get; }

    public GetSubscribersVideosQuery(string loggedInUserId)
    {
        LoggedInUserId = loggedInUserId;
    }
    
    public class Handler : BaseHandler<GetSubscribersVideosQuery, IEnumerable<VideoModel>>
    {
        private readonly IMediator _mediator;

        public Handler(IUnitOfWork unit, IMediator mediator) : base(unit)
        {
            _mediator = mediator;
        }

        public override async Task<Response<IEnumerable<VideoModel>>> Handle(GetSubscribersVideosQuery request, CancellationToken cancellationToken)
        {
            var user = await Unit.UserRepository.GetUserById(request.LoggedInUserId);
            if (user == null)
            {
                throw new ProblemDetailsException(StatusCodes.Status404NotFound, "User not found");
            }

            var list = new List<VideoModel>();
            foreach (var id in user.Subscriptions.Select(x => x.SubscripterId))
            {
                var userVideos = await _mediator.Send(new GetUserVideosQuery(id), cancellationToken);
                list.AddRange(userVideos.Result);
            }
            
            return Success(list.OrderBy(x => x.DayOfCreation).ToList());
        }
    }
}
