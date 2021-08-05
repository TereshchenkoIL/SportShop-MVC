using Microsoft.AspNetCore.Identity;
using MySportShop.Models;
using MySportShop.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MySportShop
{
    public class RoleInitializer
    {
        public static async Task InitializeAsync(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            string adminEmail = "adminPass_123456@gmail.com";
            string password = "Pass_123456";
            if (await roleManager.FindByNameAsync(WC.AdminRole) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(WC.AdminRole));
            }
            if (await roleManager.FindByNameAsync(WC.CustomerRole) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(WC.CustomerRole));
            }
            if (await userManager.FindByNameAsync(adminEmail) == null)
            {
                AppUser admin = new AppUser { Email = adminEmail, UserName = adminEmail };
                IdentityResult result = await userManager.CreateAsync(admin, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "admin");
                }
            }
        }
    }
}
