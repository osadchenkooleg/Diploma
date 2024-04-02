using Microsoft.EntityFrameworkCore;
using VideoHosting_Back_end.Data.Entities;
using VideoHosting_Back_end.Database.Abstraction;
using VideoHosting_Back_end.Database.Context;

namespace VideoHosting_Back_end.Database.Repositories;
public class VideoRepository : IVideoRepository
{
    private readonly ApplicationContext _context;

    public VideoRepository(ApplicationContext context)
    {
        _context = context;
    }

    public async Task<Video?> GetVideoById(Guid id)
    {
        return await _context.Videos.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<IEnumerable<Video>> GetVideos()
    {
        return await _context.Videos.ToListAsync();
    }

    public async Task<IEnumerable<Video>> GetVideosByName(string name)
    {
        return await _context.Videos.Where(x=> x.Name.ToLower().Contains(name.ToLower())).ToListAsync();
    }

    public async Task AddVideo(Video video)
    {
        await _context.Videos.AddAsync(video);
    }

    public void RemoveVideo(Video video)
    {
        _context.Videos.Remove(video);
    }
}
