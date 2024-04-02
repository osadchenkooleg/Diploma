using Hellang.Middleware.ProblemDetails;
using MediatR;
using VideoHosting_Back_end.Common.Common;
using VideoHosting_Back_end.Database.Abstraction;

namespace VideoHosting_Back_end.Application.Videos.Commands;

public class AddVideoViewCommand  : IRequest<Response<Unit>>
{
    public Guid VideoId { get; }

    public AddVideoViewCommand(Guid videoId)
    {
        VideoId = videoId;
    }

    public class Handler : BaseHandler<AddVideoViewCommand, Unit>
    {
        public Handler(IUnitOfWork unit) : base(unit)
        {
        }

        public override async Task<Response<Unit>> Handle(AddVideoViewCommand request, CancellationToken cancellationToken)
        {
            var video = await Unit.VideoRepository.GetVideoById(request.VideoId);
            if (video == null)
            {
                throw new ProblemDetailsException(StatusCodes.Status404NotFound, $"Video with id = {request.VideoId} does not exist.");
            }

            video.Views++;
            await Unit.SaveAsync();

            return Success(new Unit());
        }
    }
}
