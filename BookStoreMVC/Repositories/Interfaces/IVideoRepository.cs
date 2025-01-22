using BookStoreMVC.Models;

namespace BookStoreMVC.Repositories.Interfaces
{
    public interface IVideoRepository
    {
        //Gets all videos(mainly for the video page)
        Task<IEnumerable<Video>> GetAllAsync(Func<Video,bool> predicate);

        Task<IEnumerable<Video>> GetAllAsync();
        //gets all the videos of a given user
        Task<IEnumerable<Video>> GetAllVideosByUserAsync(string userId);
        //Get specific video by Id
        Task<Video> GetVideoAsync(string videoId);
        //Add new video
        Task AddVideoAsync(Video video);
        //Update an existing video
        Task UpdateVideoAsync(Video video);
        //Delete a video by Id
        Task DeleteVideoAsync(string videoId);
        //Check if user has already created the same video
        Task<bool> ExistsAsync(string videoId);
    }
}
