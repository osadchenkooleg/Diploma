using VideoHosting_Back_end.Application.Commentaries.Models;
using VideoHosting_Back_end.Data.Entities;

namespace VideoHosting_Back_end.Application.Commentaries.Mappers;

public static class CommentaryMapper
{
    public static CommentaryModel MapToCommentaryModel(this Commentary model)
    {
        return new CommentaryModel
        {
            Id = model.Id,
            UserId = model.User.Id,
            Content = model.Content,
            DayOfCreation = model.DayOfCreation,
            VideoId = model.Video.Id,
        };
    }
}
