using Hellang.Middleware.ProblemDetails;
using MediatR;
using VideoHosting_Back_end.Application.Commentaries.Models;
using VideoHosting_Back_end.Common.Common;
using VideoHosting_Back_end.Data.Entities;
using VideoHosting_Back_end.Database.Abstraction;

namespace VideoHosting_Back_end.Application.Commentaries.Commands;

public class CreateCommentaryCommand : IRequest<Response<Unit>>
{
    public CommentaryApplyModel Model { get; }

    public CreateCommentaryCommand(CommentaryApplyModel model)
    {
        Model = model;
    }

    public class Handler : BaseHandler<CreateCommentaryCommand, Unit>
    {
        public Handler(IUnitOfWork unit) : base(unit)
        {
        }

        public override async Task<Response<Unit>> Handle(CreateCommentaryCommand request, CancellationToken cancellationToken)
        {

            var user = await Unit.UserRepository.GetUserById(request.Model.UserId);
            if (user == null)
            {
                throw new ProblemDetailsException(StatusCodes.Status404NotFound, "User not found");
            }
            
            var video = await Unit.VideoRepository.GetVideoById(request.Model.VideoId);
            if (video == null)
            {
                throw new ProblemDetailsException(StatusCodes.Status404NotFound, "Video not found");
            }

            var commentary = new Commentary
            {
                Content = request.Model.Content,
                User = user,
                Video = video,
                DayOfCreation = DateTime.Now
            };
            Unit.CommentaryRepository.AddCommentary(commentary);
            await Unit.SaveAsync();

            return Success(new Unit());
        }
    }
}
