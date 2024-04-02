using VideoHosting_Back_end.Data.Entities;

namespace VideoHosting_Back_end.Database.Abstraction;
public interface ICommentaryRepository
{
    void AddCommentary(Commentary commentary);

    void RemoveCommentary(Commentary commentary);

    Task<IEnumerable<Commentary>?> GetCommentariesByVideoId(Guid id);

    Task<Commentary?> GetCommentaryById(Guid id);
}
