using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SocialMedia.Common;
using SocialMedia.Data.Models;
using System;
using System.IO;
using System.Threading.Tasks;

namespace SocialMedia.Data.Seeding
{
    public class AdminSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            var configuration = serviceProvider.GetRequiredService<IConfiguration>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            string username = configuration["Admin:Username"];
            string email = configuration["Admin:Email"];
            string firstName = configuration["Admin:FirstName"];
            string lastName = configuration["Admin:LastName"];

            var admin = new ApplicationUser
            {
                UserName = username,
                Email = email,
                FirstName = firstName,
                LastName = lastName,
                EmailConfirmed = true,
            };

            string password = configuration["Admin:Password"];

            await this.SeedAdminAsync(userManager, admin, password);
        }

        private async Task SeedAdminAsync(UserManager<ApplicationUser> userManager, ApplicationUser adminUser, string password)
        {
            var admin = await userManager.FindByNameAsync(adminUser.UserName);

            if (admin == null)
            {
                var result = await userManager.CreateAsync(adminUser, password);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, GlobalConstants.AdministratorRoleName);
                }
            }
        }
    }
}
