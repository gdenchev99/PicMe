namespace SocialMedia.Services.Data.Tests
{
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity;
    using SocialMedia.Common;
    using SocialMedia.Data;
    using SocialMedia.Data.Models;
    using SocialMedia.Data.Repositories;
    using SocialMedia.Services.Data.Tests.Common;
    using SocialMedia.Services.Mapping;
    using SocialMedia.Web.ViewModels.Administration;
    using Xunit;

    public class AdminServiceTests
    {
        [Fact]
        public async Task BanUserAsync_WithValidUserId_ShouldUpdateLockoutEnd()
        {
            // Arrange
            var context = InMemoryDbContext.Initiliaze();
            var usersRepository = new EfRepository<ApplicationUser>(context);
            var rolesRepository = new EfRepository<ApplicationRole>(context);
            var service = new AdminService(usersRepository, rolesRepository);
            await this.SeedUsersAndRoles(context);

            // Act
            string expectedEndDate = DateTimeOffset.UtcNow.AddYears(100).ToString();
            string actualEndDate = await service.BanUserAsync("userOneId");

            // Assert
            Assert.Equal(expectedEndDate, actualEndDate);
        }

        [Fact]
        public async Task UnbanUserAsync_WithValidUserId_ShouldUpdateLockoutEnd()
        {
            // Arrange
            var context = InMemoryDbContext.Initiliaze();
            var usersRepository = new EfRepository<ApplicationUser>(context);
            var rolesRepository = new EfRepository<ApplicationRole>(context);
            var service = new AdminService(usersRepository, rolesRepository);
            await this.SeedUsersAndRoles(context);

            // Act
            DateTimeOffset? expectedEndDate = null;
            await service.UnbanUserAsync("userTwoId");
            DateTimeOffset? actualEndDate = context.Users.FirstOrDefault(x => x.Id == "userTwoId").LockoutEnd;

            // Assert
            Assert.Equal(expectedEndDate, actualEndDate);
        }

        [Fact]
        public async Task GetUsersAsync_ShouldReturnAllUsers()
        {
            // Arrange
            this.InitilaizeMapper();
            var context = InMemoryDbContext.Initiliaze();
            var usersRepository = new EfRepository<ApplicationUser>(context);
            var rolesRepository = new EfRepository<ApplicationRole>(context);
            var service = new AdminService(usersRepository, rolesRepository);
            await this.SeedUsersAndRoles(context);

            // Act
            int expectedCount = context.Users.Count() - 1; // We should get all users minus one, since we want all users without the admin.
            var users = await service.GetUsersAsync();
            int actualCount = users.Count();

            // Assert
            Assert.Equal(expectedCount, actualCount);
        }

        [Fact]
        public async Task IsAdminAsync_WithValidUserId_ShouldReturnTrue()
        {
            // Arrange
            var context = InMemoryDbContext.Initiliaze();
            var usersRepository = new EfRepository<ApplicationUser>(context);
            var rolesRepository = new EfRepository<ApplicationRole>(context);
            var service = new AdminService(usersRepository, rolesRepository);
            await this.SeedUsersAndRoles(context);

            // Act
            bool isAdmin = await service.IsAdminAsync("userOneId");

            // Assert
            Assert.True(isAdmin);
        }

        private void InitilaizeMapper() => AutoMapperConfig.RegisterMappings(
               typeof(UsersViewModel).GetTypeInfo().Assembly,
               typeof(ApplicationUser).GetTypeInfo().Assembly);

        private async Task SeedUsersAndRoles(ApplicationDbContext context)
        {
            var adminRole = new ApplicationRole
            {
                Id = "adminRoleId",
                Name = GlobalConstants.AdministratorRoleName,
            };

            var userRole = new ApplicationRole
            {
                Id = "userRoleId",
                Name = GlobalConstants.UserRoleName,
            };

            var userOne = new ApplicationUser
            {
                Id = "userOneId",
                UserName = "userOneUsername",
            };

            var userTwo = new ApplicationUser
            {
                Id = "userTwoId",
                UserName = "userTwoUsername",
                LockoutEnd = DateTimeOffset.UtcNow.AddYears(100),
            };

            await context.Users.AddRangeAsync(userOne, userTwo);
            await context.Roles.AddRangeAsync(adminRole, userRole);
            await context.SaveChangesAsync();

            var adminUserRole = new IdentityUserRole<string>
            {
                UserId = "userOneId",
                RoleId = "adminRoleId",
            };

            var userUserRole = new IdentityUserRole<string>
            {
                UserId = "userTwoId",
                RoleId = "userRoleId",
            };

            await context.UserRoles.AddRangeAsync(adminUserRole, userUserRole);
            await context.SaveChangesAsync();
        }
    }
}
