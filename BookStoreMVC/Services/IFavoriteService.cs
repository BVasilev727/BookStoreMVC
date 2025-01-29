using BookStoreMVC.Models;

namespace BookStoreMVC.Services
{
    public interface IFavoriteService
    {
        Task AddToFavoriteAsync(string userId, string videoId);
        Task RemoveFromFavorites(string userId, string videoId);
        Task<IEnumerable<Video>> GetUserFavorites(string userId);
        Task<bool> IsFavoriteAsync(string userId,string videoId);
    }
}
