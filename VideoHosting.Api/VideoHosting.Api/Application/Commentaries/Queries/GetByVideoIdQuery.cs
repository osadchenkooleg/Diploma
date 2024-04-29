using Hellang.Middleware.ProblemDetails;
using MediatR;
using VideoHosting.Api.Application.Commentaries.Mappers;
using VideoHosting.Api.Application.Commentaries.Models;
using VideoHosting.Api.Common;
using VideoHosting.Common.Responses;
using VideoHosting.Database.Abstraction;

namespace VideoHosting.Api.Application.Commentaries.Queries;

public class GetByVideoIdQuery : IRequest<Response<IEnumerable<CommentaryModel>>>
{
    public Guid Id { get; set; }

    public GetByVideoIdQuery(Guid id)
    {
        Id = id;
    }

    public class Handler : BaseHandler<GetByVideoIdQuery, IEnumerable<CommentaryModel>>
    {
        public Handler(IUnitOfWork unit) : base(unit)
        {
        }

        public override async Task<Response<IEnumerable<CommentaryModel>>> Handle(GetByVideoIdQuery request, CancellationToken cancellationToken)
        {
            var video = await Unit.VideoRepository.GetVideoById(request.Id);
            
            if (video == null)
            {
                throw new ProblemDetailsException(StatusCodes.Status404NotFound, "Video doesn't exist");
            }
            
            var commentaries = video.Commentaries;
            var result = commentaries.Select(x => x.MapToCommentaryModel());

            return Success(result);
        }
    }
}
