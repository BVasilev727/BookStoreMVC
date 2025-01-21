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
    }
}
