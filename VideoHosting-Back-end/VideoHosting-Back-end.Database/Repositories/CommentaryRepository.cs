using Microsoft.EntityFrameworkCore;
using VideoHosting_Back_end.Data.Entities;
using VideoHosting_Back_end.Database.Abstraction;
using VideoHosting_Back_end.Database.Context;

namespace VideoHosting_Back_end.Database.Repositories;
public class CommentaryRepository : ICommentaryRepository
{
    private readonly ApplicationContext _context;

    public CommentaryRepository(ApplicationContext context)
    {
        _context = context;
    }

    public void AddCommentary(Commentary commentary)
    {
        _context.Commentaries.Add(commentary);
    }

    public async Task<IEnumerable<Commentary>?> GetCommentariesByVideoId(Guid id)
    {
        var com = await _context.Videos.FirstOrDefaultAsync(x => x.Id == id);
        return com?.Commentaries.ToList();
    }

    public async Task<Commentary?> GetCommentaryById(Guid id)
    {
        return await _context.Commentaries.FirstOrDefaultAsync(x => x.Id == id);
    }

    public void RemoveCommentary(Commentary commentary)
    {
        _context.Commentaries.Remove(commentary);
    }
}
