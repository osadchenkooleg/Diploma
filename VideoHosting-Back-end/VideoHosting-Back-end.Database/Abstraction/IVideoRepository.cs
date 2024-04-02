using VideoHosting_Back_end.Data.Entities;

namespace VideoHosting_Back_end.Database.Abstraction;
public interface IVideoRepository
{
    Task AddVideo(Video video);

    void RemoveVideo(Video video);

    Task<IEnumerable<Video>> GetVideosByName(string name);

    Task<IEnumerable<Video>> GetVideos();

    Task<Video?> GetVideoById(Guid id);
        
}
