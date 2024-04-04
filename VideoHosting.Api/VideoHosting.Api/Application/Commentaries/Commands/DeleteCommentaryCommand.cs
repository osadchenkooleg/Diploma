using Hellang.Middleware.ProblemDetails;
using MediatR;
using VideoHosting.Api.Common;
using VideoHosting.Common.Responses;
using VideoHosting.Database.Abstraction;

namespace VideoHosting.Api.Application.Commentaries.Commands;

public class DeleteCommentaryCommand : IRequest<Response<Unit>>
{
    public Guid Id { get; }
    public string LoggedInUserId { get; }
    public bool IsAdmin { get; }

    public DeleteCommentaryCommand(Guid id, string loggedInUserId, bool isAdmin)
    {
        Id = id;
        LoggedInUserId = loggedInUserId;
        IsAdmin = isAdmin;
    }

    public class Handler : BaseHandler<DeleteCommentaryCommand, Unit>
    {
        public Handler(IUnitOfWork unit) : base(unit)
        {
        }

        public override async Task<Response<Unit>> Handle(DeleteCommentaryCommand request, CancellationToken cancellationToken)
        {
            var commentary = await Unit.CommentaryRepository.GetCommentaryById(request.Id);
            if (commentary == null)
            {
                throw new ProblemDetailsException(StatusCodes.Status404NotFound, "Commentary doesn't exist");
            }

            if (commentary.User.Id != request.LoggedInUserId && !request.IsAdmin)
            {
                throw new ProblemDetailsException(StatusCodes.Status401Unauthorized, "Unauthorized");
            }

            Unit.CommentaryRepository.RemoveCommentary(commentary);
            await Unit.SaveAsync();

            return Success(new Unit());
        }
    }
}
