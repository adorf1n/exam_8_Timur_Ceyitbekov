using Microsoft.AspNetCore.Identity;
using MyChat.Models;

namespace MyChat.Services;

public class AdminInitializer
{
    public static async Task SeedRolesAndAdmin(RoleManager<IdentityRole<int>> _roleManager, UserManager<User> _userManager)
    {
        string adminEmail = "admin@admin.admin";
        string adminUserName = "AdminAdmin";
        string adminPassword = "AdminAdmin";
        string adminAvatar = "https://i.natgeofe.com/n/548467d8-c5f1-4551-9f58-6817a8d2c45e/NationalGeographic_2572187_2x1.jpg";
        DateTime adminDateOfBirth = new DateTime(2001, 09, 13).ToUniversalTime();
        
        var roles = new [] { "admin", "user" };
        
        foreach (var role in roles)
        {
            if (await _roleManager.FindByNameAsync(role) == null)
                await _roleManager.CreateAsync(new IdentityRole<int>(role));
        }
        if (await _userManager.FindByNameAsync(adminEmail) == null)
        {
            User admin = new User { Email = adminEmail, UserName = adminUserName, Avatar = adminAvatar, DateOfBirth = adminDateOfBirth };
            IdentityResult result = await _userManager.CreateAsync(admin, adminPassword);
            if (result.Succeeded)
                await _userManager.AddToRoleAsync(admin, "admin");
        }
    }
}