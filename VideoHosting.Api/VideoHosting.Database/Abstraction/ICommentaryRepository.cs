using VideoHosting.Data.Entities;

namespace VideoHosting.Database.Abstraction;
public interface ICommentaryRepository
{
    void AddCommentary(Commentary commentary);

    void RemoveCommentary(Commentary commentary);

    Task<IEnumerable<Commentary>?> GetCommentariesByVideoId(Guid id);

    Task<Commentary?> GetCommentaryById(Guid id);
}
