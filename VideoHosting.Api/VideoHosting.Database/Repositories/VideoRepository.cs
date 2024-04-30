using Microsoft.EntityFrameworkCore;
using VideoHosting.Data.Entities;
using VideoHosting.Database.Abstraction;
using VideoHosting.Database.Context;

namespace VideoHosting.Database.Repositories;
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
        if (video.Likes.Any())
        {
            foreach (var like in video.Likes)
            {
                _context.Likes.Remove(like);
            }
        }
        if (video.Dislikes.Any())
        {
            foreach (var like in video.Dislikes)
            {
                _context.Dislikes.Remove(like);
            }
        }
        _context.Videos.Remove(video);
    }
}
