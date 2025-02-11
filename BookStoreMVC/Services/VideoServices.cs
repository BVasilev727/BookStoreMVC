using BookStoreMVC.Models;
using BookStoreMVC.Repositories.Interfaces;

namespace BookStoreMVC.Services
{
    public class VideoServices : IVideoServices
    {
        private readonly IVideoRepository _videoRepository;

        public VideoServices(IVideoRepository videoRepository)
        {
            _videoRepository = videoRepository;
        }

        public async Task<IEnumerable<Video>> GetAllVideosAsync()
        {
            return await _videoRepository.GetAllAsync();
        }

        public async Task<IEnumerable<Video>> GetVideosByUserAsync(string userId)
        {
            return await _videoRepository.GetAllVideosByUserAsync(userId);
        }

        public async Task<Video> GetVideoAsync(string videoId)
        {
            return await _videoRepository.GetVideoAsync(videoId);
        }

        public async Task AddVideoAsync(Video video)
        {
            await _videoRepository.AddVideoAsync(video);
        }

        public async Task DeleteVideoAsync(string videoId)
        {
            await _videoRepository.DeleteVideoAsync(videoId);
        }

        public async Task UpdateVideoAsync(Video video)
        {
            await _videoRepository.UpdateVideoAsync(video);
        }

        public async Task<bool> ExistsAsync(string videoId)
        {
            return await _videoRepository.ExistsAsync(videoId);
        }

        public async Task<IEnumerable<Video>> SearchForVideos(string searchTerm)
        {
            return await _videoRepository.GetAllAsync(v => v.Title.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)
            || v.Description.Contains(searchTerm,StringComparison.OrdinalIgnoreCase));
        }

        public async Task<IEnumerable<Video>> SearchForVideosFromUser(string searchTerm, string userId)
        {
            return await _videoRepository.GetAllAsync(v => (v.Title.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)
            || v.Description.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)) && v.PostedByUser == userId);
        }

        public async Task<Video> GetRandomVideo()
        {
            return await _videoRepository.GetRandomVideoAsync();
        }
    }
}
