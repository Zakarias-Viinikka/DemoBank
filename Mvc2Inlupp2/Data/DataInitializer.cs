using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mvc2Inlupp2.Data
{
    public class DataInitializer
    {
        public void SeedData(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            context.Database.Migrate();
            SeedRoles(context);
            SeedUsers(userManager, context);
        }
        public void SeedRoles(ApplicationDbContext context)
        {
            var RolesToSeed = new List<string>();
            RolesToSeed.Add("Cashier");
            RolesToSeed.Add("Admin");

            foreach (var roleName in RolesToSeed)
            {
                var role = context.Roles.FirstOrDefault(r => r.Name == roleName);
                if (role == null)
                {
                    context.Roles.Add(new IdentityRole
                    {
                        Name = roleName,
                        NormalizedName = roleName
                    });
                    context.SaveChanges();
                }
            }
        }

        public void SeedUsers(UserManager<IdentityUser> userManager, ApplicationDbContext context)
        {
            AddUserIfNotExists(userManager, "stefan.holmberg@systementor.se", "Hejsan123#", new string[] { "Admin" });
            AddUserIfNotExists(userManager, "stefan.holmberg@nackademin.se", "Hejsan123#", new string[] { "Cashier" });

        }
        private static async void AddUserIfNotExists(UserManager<IdentityUser> userManager,
            string userName, string password, string[] roles)
        {

            if (userManager.FindByEmailAsync(userName).Result != null) return;

            var user = new IdentityUser
            {
                UserName = userName,
                Email = userName,
                EmailConfirmed = true
            };
            var userResult = userManager.CreateAsync(user, password).Result;
            var result = userManager.AddToRolesAsync(user, roles).Result;
        }
    }
}
