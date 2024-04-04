using Hellang.Middleware.ProblemDetails;
using MediatR;
using VideoHosting.Api.Application.Commentaries.Mappers;
using VideoHosting.Api.Application.Commentaries.Models;
using VideoHosting.Api.Common;
using VideoHosting.Common.Responses;
using VideoHosting.Database.Abstraction;

namespace VideoHosting.Api.Application.Commentaries.Queries;

public class GetByVideoIdQuery : IRequest<Response<CommentaryModel>>
{
    public Guid Id { get; set; }

    public GetByVideoIdQuery(Guid id)
    {
        Id = id;
    }

    public class Handler : BaseHandler<GetByVideoIdQuery, CommentaryModel>
    {
        public Handler(IUnitOfWork unit) : base(unit)
        {
        }

        public override async Task<Response<CommentaryModel>> Handle(GetByVideoIdQuery request, CancellationToken cancellationToken)
        {
            var commentary = await Unit.CommentaryRepository.GetCommentaryById(request.Id);
            if (commentary == null)
            {
                throw new ProblemDetailsException(StatusCodes.Status404NotFound, "Commentary doesn't exist");
            }
            
            return Success(commentary.MapToCommentaryModel());
        }
    }
}
