using BookStoreMVC.Models;
using BookStoreMVC.Repositories.Implementation;
using BookStoreMVC.Repositories.Interfaces;

namespace BookStoreMVC.Services
{
    public class FavoriteService : IFavoriteService
    {
        public readonly IFavoriteRepository _favRepository;

        public FavoriteService(IFavoriteRepository favoriteRepository)
        {
            _favRepository = favoriteRepository;
        }

        public async Task AddToFavoriteAsync(string userId, string videoId)
        {
            await _favRepository.AddFavoriteAsync(userId, videoId);
        }

        public async Task<IEnumerable<Video>> GetUserFavorites(string userId)
        {
            return await _favRepository.GetFavoriteVideosAsync(userId);
        }

        public async Task RemoveFromFavorites(string userId, string videoId)
        {
            await _favRepository.RemoveFavoriteAsync(userId, videoId);
        }
        public async Task<bool> IsFavoriteAsync(string userId, string videoId)
        {
            return await _favRepository.IsFavoriteAsync(userId, videoId);
        }
    }
}
