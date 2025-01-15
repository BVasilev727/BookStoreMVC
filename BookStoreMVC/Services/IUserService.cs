using Microsoft.AspNetCore.Identity;

namespace BookStoreMVC.Services
{
    public interface IUserService
    {
        Task<IEnumerable<IdentityUser>> GetAllUsersAsync();
        Task<IdentityUser> GetUserByIdAsync(string userId);
        Task<bool> DeleteUserAsync(string userId);
        Task<bool> AssignRoleAsync(string userId, string role);
    }
}
