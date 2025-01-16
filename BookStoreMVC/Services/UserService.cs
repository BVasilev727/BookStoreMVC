using BookStoreMVC.Areas.Identity.Data;
using BookStoreMVC.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace BookStoreMVC.Services
{
    [Authorize(Roles = "Admin")]
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly UserManager<BookStoreMVCUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserService(IUserRepository userRepository, UserManager<BookStoreMVCUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userRepository = userRepository;
            _userManager = userManager;
        }

        public async Task<IEnumerable<IdentityUser>> GetAllUsersAsync()
        {
            return await _userRepository.GetAllUsersAsync();
        }
        public async Task<IdentityUser> GetUserByIdAsync(string userId)
        {
            return await _userRepository.GetUserByIdAsync(userId);
        }

        public async Task<bool> DeleteUserAsync(string userId)
        {
            return await _userRepository.DeleteUserAsync(userId);
        }

        public async Task<bool> AssignRoleAsync(string userId, string role)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return false;
            
            var result = await _userManager.AddToRoleAsync(user, role);
            return result.Succeeded;
        }

        public async Task<IEnumerable<string>> GetRolesOfUser(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            return await _userManager.GetRolesAsync(user);
        }
    }

}
