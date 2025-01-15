using BookStoreMVC.Data;
using BookStoreMVC.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BookStoreMVC.Repositories.Implementation
{
    public class UserRepository : IUserRepository
    {
        private readonly BookStoreMVCDBContext _context;

        public UserRepository(BookStoreMVCDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<IdentityUser>> GetAllUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<IdentityUser> GetUserByIdAsync(string userId)
        {
            return await _context.Users.FindAsync(userId);
        }

        public async Task<IdentityUser> GetUserByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<bool> AddUserAsync(IdentityUser user)
        {
            _context.Users.Add((Areas.Identity.Data.BookStoreMVCUser)user);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateUserAsync(IdentityUser user)
        {
            _context.Users.Update((Areas.Identity.Data.BookStoreMVCUser)user);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteUserAsync(string userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user != null)
            {
                _context.Users.Remove(user);
                return await _context.SaveChangesAsync() > 0;
            }
            return false;
        }
    }
}
