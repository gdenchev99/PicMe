namespace SocialMedia.Services.Data.Tests
{
    using Microsoft.AspNetCore.Http;
    using Moq;
    using SocialMedia.Data;
    using SocialMedia.Data.Models;
    using SocialMedia.Data.Repositories;
    using SocialMedia.Services.Data.Tests.Common;
    using SocialMedia.Services.Mapping;
    using SocialMedia.Web.ViewModels.Posts;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using Xunit;

    public class PostServiceTests
    {
        [Fact]
        public async Task CreateAsync_WithValidData_ShouldReturnTrue()
        {
            // Arrange
            var context = InMemoryDbContext.Initiliaze();
            var postRepository = new EfDeletableEntityRepository<Post>(context);
            var postService = new PostsService(postRepository);

            // Act
            var model = new PostCreateModel
            {
                CreatorId = "creatorIdStringUnique",
                Description = "descriptionrandom",
            };
            var result = await postService.CreateAsync(model, "urltoimg.com", "dsahu2171dyh");

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task CreateAsync_WithValidData_ShouldAddToDatabase()
        {
            // Arrange
            var context = InMemoryDbContext.Initiliaze();
            var postRepository = new EfDeletableEntityRepository<Post>(context);
            var postService = new PostsService(postRepository);

            // Act
            var model = new PostCreateModel
            {
                CreatorId = "creatorIdStringUnique",
                Description = "descriptionrandom",
            };

            await postService.CreateAsync(model, "urltoimg.com", "dsahu2171dyh");
            int expectedCount = postRepository.All().Count();
            int actualCount = postRepository.All().Count();

            var expectedPost = model;
            var actualPost = postRepository.All().FirstOrDefault();

            // Assert
            Assert.Equal(expectedCount, actualCount);
            Assert.True(expectedPost.Description == actualPost.Description);
            Assert.True(expectedPost.CreatorId == actualPost.CreatorId);
        }

        [Fact]
        public async Task DeleteAsync_WithValidData_ShouldReturnTrue()
        {
            // Arrange
            var context = InMemoryDbContext.Initiliaze();
            var postRepository = new EfDeletableEntityRepository<Post>(context);
            var postService = new PostsService(postRepository);

            await postRepository.AddAsync(new Post { Id = 52, CreatorId = "testId", MediaSource = "dsadsa" });
            await postRepository.SaveChangesAsync();

            // Act
            var result = await postService.DeleteAsync(52);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task DeleteAsync_WithValidData_ShouldRemovePostFromDatabase()
        {
            // Arrange
            var context = InMemoryDbContext.Initiliaze();
            var postRepository = new EfDeletableEntityRepository<Post>(context);
            var postService = new PostsService(postRepository);

            await postRepository.AddAsync(new Post { Id = 52, CreatorId = "testId", MediaSource = "dsadsa" });
            await postRepository.SaveChangesAsync();

            // Act
            await postService.DeleteAsync(52);

            int expectedCount = 0;
            int actualCount = postRepository.All().Count();

            // Assert
            Assert.Equal(expectedCount, actualCount);
        }

        [Fact]
        public async Task GetFeedAsync_WithValidData_ShouldReturnFeedPosts()
        {
            // Arrange
            this.InitilaizeMapper();
            var context = InMemoryDbContext.Initiliaze();
            var postRepository = new EfDeletableEntityRepository<Post>(context);
            var postService = new PostsService(postRepository);
            await this.SeedUserFollower(context);
            await postRepository.AddAsync(new Post { Creator = context.Users.FirstOrDefault(), Description = "feedDescription" });
            await postRepository.SaveChangesAsync();

            // Act
            var feedPosts = await postService.GetFeedAsync("followerId", 0, 2);
            var expectedPostDescription = postRepository.All().FirstOrDefault().Description;
            var actualPostDescription = feedPosts.FirstOrDefault().Description;

            // Assert
            Assert.Equal(expectedPostDescription, actualPostDescription);
        }

        [Fact]
        public async Task GetAsync_WithValidData_ShouldReturnPost()
        {
            // Arrange
            var context = InMemoryDbContext.Initiliaze();
            var postRepository = new EfDeletableEntityRepository<Post>(context);
            var postService = new PostsService(postRepository);
            await postRepository.AddAsync(new Post { Id = 52, Description = "Post Description" });
            await postRepository.SaveChangesAsync();

            // Act
            var post = await postService.GetAsync(52);
            var expectedDescription = postRepository.All().FirstOrDefault().Description;
            var actualDescription = post.Description;

            // Assert
            Assert.Equal(expectedDescription, actualDescription);
        }

        [Fact]
        public async Task GetProfilePostsAsync_WithValidData_ShouldReturnProfilePosts()
        {
            // Arrange
            this.InitilaizeMapper();
            var context = InMemoryDbContext.Initiliaze();
            var postRepository = new EfDeletableEntityRepository<Post>(context);
            var postService = new PostsService(postRepository);
            await this.SeedUserFollower(context);
            await postRepository.AddAsync(new Post {Id = 52, Creator = context.Users.FirstOrDefault() });
            await postRepository.SaveChangesAsync();

            // Act
            var posts = await postService.GetProfilePostsAsync("testUser"); // "testUser" is seeded with SeedUserFollower.
            var expectedPostId = postRepository.All().FirstOrDefault().Id;
            var actualPostId = posts.FirstOrDefault().Id;

            // Assert
            Assert.Equal(expectedPostId, actualPostId);
        }

        [Fact]
        public async Task ExistsAsync_WithValidId_ShouldReturnTrue()
        {
            // Arrange
            var context = InMemoryDbContext.Initiliaze();
            var postRepository = new EfDeletableEntityRepository<Post>(context);
            var postService = new PostsService(postRepository);
            await postRepository.AddAsync(new Post { Id = 52 });
            await postRepository.SaveChangesAsync();

            // Act
            bool actualResult = await postService.ExistsAsync(52);

            // Assert
            Assert.True(actualResult);
        }

        /* Private methods for testing. */
        private void InitilaizeMapper() => AutoMapperConfig.RegisterMappings(
                typeof(FeedViewModel).GetTypeInfo().Assembly,
                typeof(ProfilePostViewModel).GetTypeInfo().Assembly,
                typeof(Post).GetTypeInfo().Assembly);

        private async Task SeedUserFollower(ApplicationDbContext context)
        {
            var follower = new UserFollower
            {
                UserId = "userId",
                FollowerId = "followerId",
                IsApproved = true,
            };

            await context.UserFollowers.AddAsync(follower);

            await context.Users.AddAsync(new ApplicationUser
            {
                Id = "userId",
                UserName = "testUser",
            });

            await context.SaveChangesAsync();
        }
    }
}
