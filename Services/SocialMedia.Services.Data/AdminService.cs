namespace SocialMedia.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using SocialMedia.Common;
    using SocialMedia.Data.Common.Repositories;
    using SocialMedia.Data.Models;
    using SocialMedia.Services.Mapping;
    using SocialMedia.Web.ViewModels.Administration;

    public class AdminService : IAdminService
    {
        private readonly IRepository<ApplicationUser> usersRepository;
        private readonly IRepository<ApplicationRole> rolesRepository;

        public AdminService(
            IRepository<ApplicationUser> usersRepository,
            IRepository<ApplicationRole> rolesRepository)
        {
            this.usersRepository = usersRepository;
            this.rolesRepository = rolesRepository;
        }

        public async Task<string> BanUserAsync(string userId)
        {
            var user = await this.usersRepository.All()
                .FirstOrDefaultAsync(u => u.Id == userId);

            // Ban user for 100 years(permanent ban)
            if (user.LockoutEnd == null)
            {
                user.LockoutEnd = DateTimeOffset.UtcNow.AddYears(100);
                this.usersRepository.Update(user);
                await this.usersRepository.SaveChangesAsync();
            }

            return user.LockoutEnd.ToString();
        }

        public async Task UnbanUserAsync(string userId)
        {
            var user = await this.usersRepository.All()
                .FirstOrDefaultAsync(u => u.Id == userId);

            // Ban user for 100 years(permanent ban)
            if (user.LockoutEnd != null)
            {
                user.LockoutEnd = null;
                await this.usersRepository.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<UsersViewModel>> GetUsersAsync()
        {
            var role = await this.rolesRepository.All().FirstOrDefaultAsync(r => r.Name == GlobalConstants.UserRoleName);

            if (role == null)
            {
                return null;
            }

            string roleId = role.Id;

            var users = await this.usersRepository.All()
                .Where(u => u.Roles.Any(r => r.RoleId == roleId))
                .OrderByDescending(u => u.CreatedOn)
                .To<UsersViewModel>()
                .ToListAsync();

            return users;
        }

        public async Task<bool> IsAdminAsync(string id)
        {
            var role = await this.rolesRepository.All().FirstOrDefaultAsync(r => r.Name == GlobalConstants.AdministratorRoleName);

            if (role == null)
            {
                return false;
            }

            string roleId = role.Id;

            bool result = await this.usersRepository.All().AnyAsync(u => u.Id == id && u.Roles.Any(r => r.RoleId == roleId));

            return result;
        }
    }
}
