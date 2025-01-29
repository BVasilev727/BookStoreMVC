using BookStoreMVC.Data;
using BookStoreMVC.Models;
using BookStoreMVC.Repositories.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace BookStoreMVC.Repositories.Implementation
{
    public class FavoriteRepository : IFavoriteRepository
    {
        private readonly BookStoreMVCDBContext _context;

        public FavoriteRepository(BookStoreMVCDBContext context)
        {
            _context = context;
        }

        public async Task AddFavoriteAsync(string userId, string videoId)
        {
            if (!await _context.FavoriteVideos.AnyAsync(fv => fv.UserId == userId && fv.VideoId == videoId))
            {
                var favorite = new FavoriteVideo { UserId = userId, VideoId = videoId };
                _context.FavoriteVideos.Add(favorite);
                await _context.SaveChangesAsync();
            }
        }

        public async Task RemoveFavoriteAsync(string userId, string videoId)
        {
            var favorite = await _context.FavoriteVideos.
                FirstOrDefaultAsync(fv => fv.UserId == userId && fv.VideoId == videoId);
            
            if(favorite != null)
            {
                _context.FavoriteVideos.Remove(favorite);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> IsFavoriteAsync(string userId, string videoId)
        {
            return await _context.FavoriteVideos.AnyAsync(fv => fv.UserId == userId && fv.VideoId == videoId);  
        }

        public async Task<List<Video>> GetFavoriteVideosAsync(string userId)
        {
            return await _context.FavoriteVideos
                .Where(fv => fv.UserId == userId)
                .Select(fv => fv.Video)
                .ToListAsync();
        }
    }
}
