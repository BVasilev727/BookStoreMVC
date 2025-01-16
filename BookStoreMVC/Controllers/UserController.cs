using BookStoreMVC.Models;
using BookStoreMVC.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace BookStoreMVC.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _userService.GetAllUsersAsync();
            var userRoles = new List<UserWithRoleViewModel>();

            foreach (var user in users)
            {
                var roles = await _userService.GetRolesOfUser(user.Id);
                userRoles.Add (new UserWithRoleViewModel
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    Roles = roles.ToList()
                });
            }
            return View("~/Views/User/Index.cshtml", userRoles);
        }

        public async Task<IActionResult> Details(string id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            var roles = await _userService.GetRolesOfUser(user.Id);

            if (user == null)
            {
                return NotFound();
            }

            var userModel = new UserWithRoleViewModel
            {
                UserId = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                Roles = roles.ToList()
            };
            return View(userModel);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var result = await _userService.DeleteUserAsync(id);
            if (result)
            {
                return RedirectToAction("Index");
            }
            return BadRequest("Failed to delete user");
        }

        public async Task<IActionResult> Delete(string id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var user = await _userService.GetUserByIdAsync(id);
            var roles = await _userService.GetRolesOfUser(user.Id);

            if (user == null)
            {
                return NotFound();
            }

            var userModel = new UserWithRoleViewModel
            {
                UserId = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                Roles = roles.ToList()
            };
            return View(userModel);
        }
    }
}
