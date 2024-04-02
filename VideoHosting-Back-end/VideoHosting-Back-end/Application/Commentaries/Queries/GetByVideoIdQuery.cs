using Hellang.Middleware.ProblemDetails;
using MediatR;
using VideoHosting_Back_end.Application.Commentaries.Mappers;
using VideoHosting_Back_end.Application.Commentaries.Models;
using VideoHosting_Back_end.Common.Common;
using VideoHosting_Back_end.Database.Abstraction;

namespace VideoHosting_Back_end.Application.Commentaries.Queries;

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
