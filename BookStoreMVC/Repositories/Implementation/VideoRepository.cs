using BookStoreMVC.Data;
using BookStoreMVC.Models;
using BookStoreMVC.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookStoreMVC.Repositories.Implementation
{
    public class VideoRepository : IVideoRepository
    {
        private readonly BookStoreMVCDBContext _context;
        public VideoRepository(BookStoreMVCDBContext context) {
            _context = context;
        }

        public async Task<IEnumerable<Video>> GetAllAsync(Func<Video, bool> predicate = null)
        {
            if(predicate == null)
            {
                return await _context.Videos.ToListAsync();
            }
            return _context.Videos.Where(predicate).ToList();
        }
        public async Task<IEnumerable<Video>> GetAllAsync()
        {
            return await _context.Videos.ToListAsync();
        }
        public async Task<IEnumerable<Video>> GetAllVideosByUserAsync(string userId)
        {
            return await _context.Videos.Where(v => v.PostedByUser == userId).ToListAsync();
        }

        public async Task<Video> GetVideoAsync(string videoId)
        {
            return await _context.Videos.FirstOrDefaultAsync(v => v.Id == videoId);
        }

        public async Task AddVideoAsync(Video video)
        {
            await _context.Videos.AddAsync(video);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateVideoAsync(Video video)
        {
            _context.Videos.Update(video);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteVideoAsync(string videoId)
        {
            var video = await _context.Videos.FindAsync(videoId);
            if(video!=null)
            {
                _context.Videos.Remove(video);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<bool> ExistsAsync(string videoId)
        {
            return await _context.Videos.AnyAsync(v => v.Id == videoId);
        }
        public async Task<Video> GetRandomVideoAsync()
        {
            var videos = await _context.Videos.ToListAsync();
            return videos.OrderBy(v => Guid.NewGuid()).FirstOrDefault();
        }
    }
}
