using VideoHosting.Data.Entities;

namespace VideoHosting.Database.Abstraction;
public interface IVideoRepository
{
    Task AddVideo(Video video);

    void RemoveVideo(Video video);

    Task<IEnumerable<Video>> GetVideosByName(string name);

    Task<IEnumerable<Video>> GetVideos();

    Task<Video?> GetVideoById(Guid id);
        
}
