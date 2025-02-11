using BookStoreMVC.Models;

namespace BookStoreMVC.Services
{
    public interface IVideoServices
    {
        Task<IEnumerable<Video>> GetAllVideosAsync();
        Task<IEnumerable<Video>> GetVideosByUserAsync(string userId);
        Task<Video> GetVideoAsync(string videoId);
        Task AddVideoAsync(Video video);
        Task DeleteVideoAsync(string videoId);
        Task UpdateVideoAsync(Video video);
        Task<bool> ExistsAsync(string videoId);
        Task<IEnumerable<Video>> SearchForVideos(string seachTerm);
        Task<IEnumerable<Video>> SearchForVideosFromUser(string searchTerm, string userId);

        Task<Video> GetRandomVideo();
    }
}
