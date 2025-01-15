using BookStoreMVC.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Data;
using System.Linq;

namespace BookStoreMVC.Areas.Identity.Data
{
    public class SeedRolesAndAdmin
    {
        public static async Task SeedRolesAndAdminUser(IServiceProvider serviceProvider)
        {
            try
            {
                var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                string[] roleNames = { "Admin", "User" };

                foreach (var roleName in roleNames)
                {
                    if (!await roleManager.RoleExistsAsync(roleName))
                    {
                        await roleManager.CreateAsync(new IdentityRole(roleName));
                    }
                }
                var userManager = serviceProvider.GetRequiredService<UserManager<BookStoreMVCUser>>();

                string adminEmail = "admin@gmail.com";
                string adminRoleName = "Admin";

                var adminUser = await userManager.FindByEmailAsync(adminEmail);
                if (adminUser == null)
                {
                    throw new Exception("no admin user found");
                }
                else
                {
                    if (!await userManager.IsInRoleAsync(adminUser, adminRoleName))
                    {
                        var roleClaimed = await userManager.AddToRoleAsync(adminUser, adminRoleName);
                        if (!roleClaimed.Succeeded)
                        {
                            throw new Exception($"Failed to assign Admin role to user: {string.Join(", ", roleClaimed.Errors.Select(e => e.Description))}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error durring seeding {ex.Message}");
            }
            
        }
    }
}
