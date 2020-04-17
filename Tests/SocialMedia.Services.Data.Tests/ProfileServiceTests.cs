namespace SocialMedia.Services.Data.Tests
{
    using SocialMedia.Data;
    using SocialMedia.Data.Models;
    using SocialMedia.Data.Repositories;
    using SocialMedia.Services.Data.Tests.Common;
    using SocialMedia.Services.Mapping;
    using SocialMedia.Web.ViewModels.Profiles;
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;

    using Xunit;

    public class ProfileServiceTests
    {
        [Fact]
        public async Task AddFollowerAsync_WithValidData_ShouldReturnTrue()
        {
            // Arrange
            var context = InMemoryDbContext.Context();
            var userRepository = new EfDeletableEntityRepository<ApplicationUser>(context);
            var userFollowerRepository = new EfRepository<UserFollower>(context);
            var profileService = new ProfilesService(userRepository, userFollowerRepository);
            await this.SeedUsers(context);

            var model = new AddFollowerModel
            {
                UserId = "userOneId",
                FollowerId = "userTwoId",
            };

            // Act
            bool actualResult = await profileService.AddFollowerAsync(model);

            // Assert
            Assert.True(actualResult);
        }

        [Fact]
        public async Task AddFollowerAsync_WithValidData_ShouldAddFollowerToDatabase()
        {
            // Arrange
            var context = InMemoryDbContext.Context();
            var userRepository = new EfDeletableEntityRepository<ApplicationUser>(context);
            var userFollowerRepository = new EfRepository<UserFollower>(context);
            var profileService = new ProfilesService(userRepository, userFollowerRepository);
            await this.SeedUsers(context);

            var model = new AddFollowerModel
            {
                UserId = "userOneId",
                FollowerId = "userTwoId",
            };

            // Act
            int expectedCount = userFollowerRepository.All().Count() + 1;
            await profileService.AddFollowerAsync(model);
            int actualCount = userFollowerRepository.All().Count();

            // Assert
            Assert.Equal(expectedCount, actualCount);
        }

        [Fact]
        public async Task GetUserProfileAsync_WithValidUsername_ShouldReturnUserProfile()
        {
            // Arrange
            this.InitilaizeMapper();
            var context = InMemoryDbContext.Context();
            var userRepository = new EfDeletableEntityRepository<ApplicationUser>(context);
            var userFollowerRepository = new EfRepository<UserFollower>(context);
            var profileService = new ProfilesService(userRepository, userFollowerRepository);
            await this.SeedUsers(context);

            // Act
            var profile = await profileService.GetUserProfileAsync("userOneUsername");
            string expectedId = userRepository.All().FirstOrDefault(x => x.UserName == "userOneUsername").Id;
            string actualId = profile.Id;

            // Assert
            Assert.Equal(expectedId, actualId);
        }

        [Fact]
        public async Task RemoveFollowerAsync_WithValidData_ShouldRemoveFollowerFromDatabase()
        {
            // Arrange
            this.InitilaizeMapper();
            var context = InMemoryDbContext.Context();
            var userRepository = new EfDeletableEntityRepository<ApplicationUser>(context);
            var userFollowerRepository = new EfRepository<UserFollower>(context);
            var profileService = new ProfilesService(userRepository, userFollowerRepository);
            await this.SeedUsers(context);

            await userFollowerRepository.AddAsync(new UserFollower { UserId = "userOneId", FollowerId = "userTwoId" });
            await userFollowerRepository.SaveChangesAsync();
            var model = new AddFollowerModel
            {
                UserId = "userOneId",
                FollowerId = "userTwoId",
            };

            // Act
            int expectedCount = userFollowerRepository.All().Count() - 1;
            await profileService.RemoveFollowerAsync(model);
            int actualCount = userFollowerRepository.All().Count();

            // Assert
            Assert.Equal(expectedCount, actualCount);
        }

        [Fact]
        public async Task GetUserFollowersAsync_WithValidData_ShouldReturnUserFollowers()
        {
            // Arrange
            this.InitilaizeMapper();
            var context = InMemoryDbContext.Context();
            var userRepository = new EfDeletableEntityRepository<ApplicationUser>(context);
            var userFollowerRepository = new EfRepository<UserFollower>(context);
            var profileService = new ProfilesService(userRepository, userFollowerRepository);
            await this.SeedUsers(context);

            await userFollowerRepository.AddAsync(new UserFollower { UserId = "userOneId", FollowerId = "userTwoId", IsApproved = true });
            await userFollowerRepository.SaveChangesAsync();

            // Act
            var followers = await profileService.GetUserFollowersAsync("userOneUsername");
            string expectedUsername = userRepository.All().FirstOrDefault(u => u.Id == "userTwoId").UserName;
            string actualUsername = followers.FirstOrDefault().FollowerUserName;

            // Assert
            Assert.Equal(expectedUsername, actualUsername);
        }

        [Fact]
        public async Task GetUserFollowingsAsync_WithValidData_ShouldReturnUserFollowings()
        {
            // Arrange
            this.InitilaizeMapper();
            var context = InMemoryDbContext.Context();
            var userRepository = new EfDeletableEntityRepository<ApplicationUser>(context);
            var userFollowerRepository = new EfRepository<UserFollower>(context);
            var profileService = new ProfilesService(userRepository, userFollowerRepository);
            await this.SeedUsers(context);

            await userFollowerRepository.AddAsync(new UserFollower { UserId = "userOneId", FollowerId = "userTwoId", IsApproved = true });
            await userFollowerRepository.SaveChangesAsync();

            // Act
            var followings = await profileService.GetUserFollowingsAsync("userTwoUsername");
            string expectedUsername = userRepository.All().FirstOrDefault(u => u.Id == "userOneId").UserName;
            string actualUsername = followings.FirstOrDefault().UserUserName;

            // Assert
            Assert.Equal(expectedUsername, actualUsername);
        }

        [Fact]
        public async Task UploadProfilePictureAsync_WithValidPicture_ShouldUpdateEntity()
        {
            // Arrange
            var context = InMemoryDbContext.Context();
            var userRepository = new EfDeletableEntityRepository<ApplicationUser>(context);
            var userFollowerRepository = new EfRepository<UserFollower>(context);
            var profileService = new ProfilesService(userRepository, userFollowerRepository);
            await this.SeedUsers(context);

            // Act
            string url = await profileService.UploadProfilePictureAsync("userOneId", "newPictureUrl", "newPictureId");
            string expectedUrl = userRepository.All().FirstOrDefault(u => u.Id == "userOneId").ProfilePictureUrl;
            string actualUrl = url;

            // Assert
            Assert.Equal(expectedUrl, actualUrl);
        }

        [Fact]
        public async Task SearchProfileAsync_WithValidSearchInput_ShouldReturnProfiles()
        {
            // Arrange
            this.InitilaizeMapper();
            var context = InMemoryDbContext.Context();
            var userRepository = new EfDeletableEntityRepository<ApplicationUser>(context);
            var userFollowerRepository = new EfRepository<UserFollower>(context);
            var profileService = new ProfilesService(userRepository, userFollowerRepository);
            await this.SeedUsers(context);

            // Act
            var profiles = await profileService.SearchProfilesAsync("user"); // Searching for profiles whose usernames start with "user".
            var expectedCount = userRepository.All().Count(); // Both profiles start with "user" so we should get 2 results.
            var actualCount = profiles.Count();

            // Assert
            Assert.Equal(expectedCount, actualCount);
        }

        [Fact]
        public async Task GetFollowerRequestsAsync_WithValidUserId_ShouldReturnFollowerRequests()
        {
            // Arrange
            this.InitilaizeMapper();
            var context = InMemoryDbContext.Context();
            var userRepository = new EfDeletableEntityRepository<ApplicationUser>(context);
            var userFollowerRepository = new EfRepository<UserFollower>(context);
            var profileService = new ProfilesService(userRepository, userFollowerRepository);
            await this.SeedUsers(context);
            await userFollowerRepository.AddAsync(new UserFollower
            { UserId = "userOneId", FollowerId = "userTwoId", IsApproved = false });
            await userFollowerRepository.SaveChangesAsync();

            // Act
            var requests = await profileService.GetFollowerRequestsAsync("userOneId");
            string expectedUsername = "userTwoUsername"; // The username of the follower.
            string actualUsername = requests.FirstOrDefault().FollowerUserName;

            // Assert
            Assert.Equal(expectedUsername, actualUsername);
        }

        [Fact]
        public async Task ApproveRequestAsync_WithValidRequesterUsername_ShouldUpdateDatabase()
        {
            // Arrange
            var context = InMemoryDbContext.Context();
            var userRepository = new EfDeletableEntityRepository<ApplicationUser>(context);
            var userFollowerRepository = new EfRepository<UserFollower>(context);
            var profileService = new ProfilesService(userRepository, userFollowerRepository);
            await this.SeedUsers(context);
            await userFollowerRepository.AddAsync(new UserFollower
            { UserId = "userOneId", FollowerId = "userTwoId", IsApproved = false });
            await userFollowerRepository.SaveChangesAsync();

            // Act
            bool result = await profileService.ApproveRequestAsync("userTwoUsername");

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task DeleteRequestAsync_WithValidReqesterUsername_ShouldRemoveFromDatabase()
        {
            // Arrange
            var context = InMemoryDbContext.Context();
            var userRepository = new EfDeletableEntityRepository<ApplicationUser>(context);
            var userFollowerRepository = new EfRepository<UserFollower>(context);
            var profileService = new ProfilesService(userRepository, userFollowerRepository);
            await this.SeedUsers(context);
            await userFollowerRepository.AddAsync(new UserFollower
            { UserId = "userOneId", FollowerId = "userTwoId", IsApproved = false });
            await userFollowerRepository.SaveChangesAsync();

            // Act
            int expectedCount = userFollowerRepository.All().Count() - 1;
            await profileService.DeleteRequestAsync("userTwoUsername");
            int actualCount = userFollowerRepository.All().Count();

            // Assert
            Assert.Equal(expectedCount, actualCount);
        }

        [Fact]
        public async Task FollowerExistsAsync_WithValidData_ShouldReturnTrue()
        {
            // Arrange
            var context = InMemoryDbContext.Context();
            var userRepository = new EfDeletableEntityRepository<ApplicationUser>(context);
            var userFollowerRepository = new EfRepository<UserFollower>(context);
            var profileService = new ProfilesService(userRepository, userFollowerRepository);
            await this.SeedUsers(context);
            await userFollowerRepository.AddAsync(new UserFollower
            { UserId = "userOneId", FollowerId = "userTwoId", IsApproved = true });
            await userFollowerRepository.SaveChangesAsync();

            // Act
            bool result = await profileService.FollowerExistsAsync("userOneId", "userTwoId");

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task UserExistsByIdAsync_WithValidUserId_ShouldReturnTrue()
        {
            // Arrange
            var context = InMemoryDbContext.Context();
            var userRepository = new EfDeletableEntityRepository<ApplicationUser>(context);
            var userFollowerRepository = new EfRepository<UserFollower>(context);
            var profileService = new ProfilesService(userRepository, userFollowerRepository);
            await this.SeedUsers(context);

            // Act
            bool result = await profileService.UserExistsByIdAsync("userOneId");

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task UserExistsByNameAsync_WithValidUserName_ShouldReturnTrue()
        {
            // Arrange
            var context = InMemoryDbContext.Context();
            var userRepository = new EfDeletableEntityRepository<ApplicationUser>(context);
            var userFollowerRepository = new EfRepository<UserFollower>(context);
            var profileService = new ProfilesService(userRepository, userFollowerRepository);
            await this.SeedUsers(context);

            // Act
            bool result = await profileService.UserExistsByNameAsync("userOneUsername");

            // Assert
            Assert.True(result);
        }

        private void InitilaizeMapper() => AutoMapperConfig.RegisterMappings(
                typeof(UserProfileViewModel).GetTypeInfo().Assembly,
                typeof(ApplicationUser).GetTypeInfo().Assembly);

        private async Task SeedUsers(ApplicationDbContext context)
        {
            var userOne = new ApplicationUser
            {
                Id = "userOneId",
                UserName = "userOneUsername",
                IsPrivate = false,
                ProfilePictureUrl = "pictureUrl",
                PicturePublicId = "pictureId",
            };

            var userTwo = new ApplicationUser
            {
                Id = "userTwoId",
                UserName = "userTwoUsername",
                IsPrivate = true,
            };

            await context.Users.AddAsync(userOne);
            await context.Users.AddAsync(userTwo);
            await context.SaveChangesAsync();
        }
    }
}
