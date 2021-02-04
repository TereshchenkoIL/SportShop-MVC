using Microsoft.AspNetCore.Identity;
using MySportShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MySportShop
{
    public class RoleInitializer
    {
        public static async Task InitializeAsync(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            string adminEmail = "admin@gmail.com";
            string password = "_Aa123456";
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
                IdentityUser admin = new IdentityUser { Email = adminEmail, UserName = adminEmail };
                IdentityResult result = await userManager.CreateAsync(admin, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "admin");
                }
            }
        }
    }
}
