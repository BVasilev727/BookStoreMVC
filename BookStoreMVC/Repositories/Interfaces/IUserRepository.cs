using Microsoft.AspNetCore.Identity;

namespace BookStoreMVC.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<IdentityUser>> GetAllUsersAsync();
        Task<IdentityUser> GetUserByIdAsync(string userId);
        Task<IdentityUser> GetUserByEmailAsync(string email);
        Task<bool> AddUserAsync(IdentityUser user);
        Task<bool> UpdateUserAsync(IdentityUser user);
        Task<bool> DeleteUserAsync(string userId);
    }
}
