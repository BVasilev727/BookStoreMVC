using BookStoreMVC.Models;

namespace BookStoreMVC.Repositories.Interfaces
{
    public interface IFavoriteRepository
    {
        Task AddFavoriteAsync(string userId, string videoId);
        Task RemoveFavoriteAsync(string userId, string videoId);
        Task<bool> IsFavoriteAsync(string userId, string videoId);
        Task<List<Video>> GetFavoriteVideosAsync(string userId);
    }
}
