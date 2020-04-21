namespace SocialMedia.Services.Data.Tests
{
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;
    using SocialMedia.Data;
    using SocialMedia.Data.Models;
    using SocialMedia.Data.Repositories;
    using SocialMedia.Services.Data.Tests.Common;
    using SocialMedia.Services.Mapping;
    using SocialMedia.Web.ViewModels.Likes;
    using Xunit;

    public class LikeServiceTests
    {
        [Fact]
        public async Task AddAsync_WithValidData_ShouldAddLikeToDatabase()
        {
            // Arrange
            var context = InMemoryDbContext.Initiliaze();
            var likesRepository = new EfRepository<Like>(context);
            var usersRepository = new EfRepository<ApplicationUser>(context);
            var postsRepository = new EfRepository<Post>(context);
            var service = new LikesService(likesRepository, usersRepository, postsRepository);
            await this.SeedUserAndPost(context);
            var model = new AddLikeModel { UserId = "userId", PostId = 52 };

            // Act
            int expectedCount = context.Likes.Count() + 1;
            await service.AddAsync(model);
            int actualCount = context.Likes.Count();

            // Assert
            Assert.Equal(expectedCount, actualCount);
        }

        [Fact]
        public async Task AddAsync_WithValidData_ShouldReturnPostCreatorId()
        {
            // Arrange
            var context = InMemoryDbContext.Initiliaze();
            var likesRepository = new EfRepository<Like>(context);
            var usersRepository = new EfRepository<ApplicationUser>(context);
            var postsRepository = new EfRepository<Post>(context);
            var service = new LikesService(likesRepository, usersRepository, postsRepository);
            await this.SeedUserAndPost(context);
            var model = new AddLikeModel { UserId = "userId", PostId = 52 };

            // Act
            string expectedPostCreatorId = context.Posts.FirstOrDefault().CreatorId;
            string actualPostCreatorId = await service.AddAsync(model);

            // Assert
            Assert.Equal(expectedPostCreatorId, actualPostCreatorId);
        }

        [Fact]
        public async Task GetLastThreeAsync_WithValidData_ShouldReturnLast3Likes()
        {
            // Arrange
            this.InitilaizeMapper();
            var context = InMemoryDbContext.Initiliaze();
            var likesRepository = new EfRepository<Like>(context);
            var usersRepository = new EfRepository<ApplicationUser>(context);
            var postsRepository = new EfRepository<Post>(context);
            var service = new LikesService(likesRepository, usersRepository, postsRepository);
            await this.SeedUserAndPost(context);
            await this.SeedLikes(context);

            // Act
            var likes = await service.GetLastThreeAsync(52);
            bool firstLikeRetrieved = likes.Any(x => x.Id == 26); // Should return false, since we require only the latest 3 likes.
            bool fourthLikeRetrieved = likes.Any(x => x.Id == 29); // Should return true.

            // Assert
            Assert.False(firstLikeRetrieved);
            Assert.True(fourthLikeRetrieved);
        }

        [Fact]
        public async Task IsPostLikedByUserAsync_WithValidData_ShouldReturnTrue()
        {
            // Arrange
            var context = InMemoryDbContext.Initiliaze();
            var likesRepository = new EfRepository<Like>(context);
            var usersRepository = new EfRepository<ApplicationUser>(context);
            var postsRepository = new EfRepository<Post>(context);
            var service = new LikesService(likesRepository, usersRepository, postsRepository);
            await this.SeedUserAndPost(context);
            await this.SeedLikes(context);

            // Act
            bool isLiked = await service.IsPostLikedByUserAsync("userId", 52);

            // Assert
            Assert.True(isLiked);
        }

        [Fact]
        public async Task IsPostLikedByUserAsync_WithInvalidPostId_ShouldReturnFalse()
        {
            // Arrange
            var context = InMemoryDbContext.Initiliaze();
            var likesRepository = new EfRepository<Like>(context);
            var usersRepository = new EfRepository<ApplicationUser>(context);
            var postsRepository = new EfRepository<Post>(context);
            var service = new LikesService(likesRepository, usersRepository, postsRepository);
            await this.SeedUserAndPost(context);
            await this.SeedLikes(context);

            // Act
            bool isLiked = await service.IsPostLikedByUserAsync("userId", 51252); // PostId is non-existent

            // Assert
            Assert.False(isLiked);
        }

        [Fact]
        public async Task IsPostLikedByUserAsync_WithInvalidUserId_ShouldReturnFalse()
        {
            // Arrange
            var context = InMemoryDbContext.Initiliaze();
            var likesRepository = new EfRepository<Like>(context);
            var usersRepository = new EfRepository<ApplicationUser>(context);
            var postsRepository = new EfRepository<Post>(context);
            var service = new LikesService(likesRepository, usersRepository, postsRepository);
            await this.SeedUserAndPost(context);
            await this.SeedLikes(context);

            // Act
            bool isLiked = await service.IsPostLikedByUserAsync("InvalidUserId", 52); // UserId is non-existent

            // Assert
            Assert.False(isLiked);
        }

        [Fact]
        public async Task RemoveAsync_WithValidData_ShouldReturnTrue()
        {
            // Arrange
            var context = InMemoryDbContext.Initiliaze();
            var likesRepository = new EfRepository<Like>(context);
            var usersRepository = new EfRepository<ApplicationUser>(context);
            var postsRepository = new EfRepository<Post>(context);
            var service = new LikesService(likesRepository, usersRepository, postsRepository);
            await this.SeedUserAndPost(context);
            await this.SeedLikes(context);
            var model = new AddLikeModel { UserId = "userId", PostId = 52 };

            // Act
            bool isLikeDeleted = await service.RemoveAsync(model);

            // Assert
            Assert.True(isLikeDeleted);
        }

        [Fact]
        public async Task ExistsAsync_WithValidLikeId_ShouldReturnTrue()
        {
            // Arrange
            var context = InMemoryDbContext.Initiliaze();
            var likesRepository = new EfRepository<Like>(context);
            var usersRepository = new EfRepository<ApplicationUser>(context);
            var postsRepository = new EfRepository<Post>(context);
            var service = new LikesService(likesRepository, usersRepository, postsRepository);
            await this.SeedUserAndPost(context);
            await this.SeedLikes(context);

            // Act
            bool likeExists = await service.ExistsAsync(26); // Valid id.
            bool likeExistsTwo = await service.ExistsAsync(59135); // Invalid id.

            // Assert
            Assert.True(likeExists);
            Assert.False(likeExistsTwo);
        }

        private void InitilaizeMapper() => AutoMapperConfig.RegisterMappings(
                typeof(LikeViewModel).GetTypeInfo().Assembly,
                typeof(Like).GetTypeInfo().Assembly);

        private async Task SeedUserAndPost(ApplicationDbContext context)
        {
            var user = new ApplicationUser
            {
                Id = "userId",
                UserName = "userName",
            };

            var userTwo = new ApplicationUser
            {
                Id = "userTwoId",
                UserName = "userTwoName",
            };

            var userThree = new ApplicationUser
            {
                Id = "userThreeId",
                UserName = "userThreeName",
            };

            var post = new Post
            {
                Id = 52,
                CreatorId = user.Id,
            };

            await context.Users.AddRangeAsync(user, userTwo, userThree);
            await context.Posts.AddAsync(post);
            await context.SaveChangesAsync();
        }

        private async Task SeedLikes(ApplicationDbContext context)
        {
            var like = new Like
            {
                Id = 26,
                UserId = "userId",
                PostId = 52,
            };

            var likeTwo = new Like
            {
                Id = 27,
                UserId = "userTwoId",
                PostId = 52,
            };

            var likeThree = new Like
            {
                Id = 28,
                UserId = "userThreeId",
                PostId = 52,
            };

            var likeFour = new Like
            {
                Id = 29,
                UserId = "userThreeId",
                PostId = 52,
            };

            await context.AddAsync(like);
            await context.SaveChangesAsync();

            await context.AddAsync(likeTwo);
            await context.SaveChangesAsync();

            await context.AddAsync(likeThree);
            await context.SaveChangesAsync();

            await context.AddAsync(likeFour);
            await context.SaveChangesAsync();
        }
    }
}
