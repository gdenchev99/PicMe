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
    using SocialMedia.Web.ViewModels.Comments;
    using Xunit;

    public class CommentServiceTests
    {
        [Fact]
        public async Task CreateAsync_WithValidData_ShouldAddCommentToDatabase()
        {
            // Arrange
            var context = InMemoryDbContext.Context();
            var commentsRepository = new EfDeletableEntityRepository<Comment>(context);
            var postsRepository = new EfDeletableEntityRepository<Post>(context);
            var service = new CommentsService(commentsRepository, postsRepository);
            await this.SeedUsersAndPost(context);
            var model = new CommentCreateModel
            {
                CreatorId = "userId",
                PostId = 52,
                Text = "Comment Text",
            };

            // Act
            int expectedCount = context.Comments.Count() + 1;
            await service.CreateAsync(model);
            int actualCount = context.Comments.Count();

            // Assert
            Assert.Equal(expectedCount, actualCount);
        }

        [Fact]
        public async Task CreateAsync_WithValidData_ShouldReturnUserIdOfCreator()
        {
            // Arrange
            var context = InMemoryDbContext.Context();
            var commentsRepository = new EfDeletableEntityRepository<Comment>(context);
            var postsRepository = new EfRepository<Post>(context);
            var service = new CommentsService(commentsRepository, postsRepository);
            await this.SeedUsersAndPost(context);
            var model = new CommentCreateModel
            {
                CreatorId = "userId",
                PostId = 52,
                Text = "Comment Text",
            };

            // Act
            string expectedUserId = context.Users.FirstOrDefault().Id;
            string actualUserId = await service.CreateAsync(model);

            // Assert
            Assert.Equal(expectedUserId, actualUserId);
        }

        [Fact]
        public async Task DeleteAsync_WithValidCommentId_ShouldDeleteCommentFromDatabase()
        {
            // Arrange
            var context = InMemoryDbContext.Context();
            var commentsRepository = new EfDeletableEntityRepository<Comment>(context);
            var postsRepository = new EfRepository<Post>(context);
            var service = new CommentsService(commentsRepository, postsRepository);
            await this.SeedUsersAndPost(context);
            await this.SeedComments(context);

            // Act
            int expectedCount = context.Comments.Count() - 1;
            await service.DeleteAsync(26); // Id is seeded with SeedComment method.
            int actualCount = context.Comments.Count();

            // Assert
            Assert.Equal(expectedCount, actualCount);
        }

        [Fact]
        public async Task GetLastTwoAsync_WithValidPostId_ShouldReturnLastTwoComments()
        {
            // Arrange
            this.InitilaizeMapper();
            var context = InMemoryDbContext.Context();
            var commentsRepository = new EfDeletableEntityRepository<Comment>(context);
            var postsRepository = new EfRepository<Post>(context);
            var service = new CommentsService(commentsRepository, postsRepository);
            await this.SeedUsersAndPost(context);
            await this.SeedComments(context);

            // Act
            var comments = await service.GetLastTwoAsync(52); // Should return second and third comments from the seeding.
            bool firstCommentRetrieved = comments.Any(x => x.Id == 26); // Id of the first comment.
            bool secondCommentRetrieved = comments.Any(x => x.Id == 27); // Id of the second comment.
            bool thirdCommentRetrieved = comments.Any(x => x.Id == 28); // Id of third comment.

            // Assert
            Assert.False(firstCommentRetrieved); // False
            Assert.True(secondCommentRetrieved); // True
            Assert.True(thirdCommentRetrieved); // True
        }

        [Fact]
        public async Task GetPostCommentsAsync_WithValidPostId_ShouldReturnPostComments()
        {
            // Arrange
            this.InitilaizeMapper();
            var context = InMemoryDbContext.Context();
            var commentsRepository = new EfDeletableEntityRepository<Comment>(context);
            var postsRepository = new EfRepository<Post>(context);
            var service = new CommentsService(commentsRepository, postsRepository);
            await this.SeedUsersAndPost(context);
            await this.SeedComments(context);

            // Act
            int takeCount = 10;
            var comments = await service.GetPostCommentsAsync(52, 0, takeCount);
            int expectedCount = context.Comments.Where(x => x.PostId == 52).Take(takeCount).Count();
            int actualCount = comments.Take(takeCount).Count();

            // Assert
            Assert.Equal(expectedCount, actualCount);
        }

        [Fact]
        public async Task ExistsAsync_WithValidId_ShouldReturnTrue()
        {
            // Arrange
            var context = InMemoryDbContext.Context();
            var commentsRepository = new EfDeletableEntityRepository<Comment>(context);
            var postsRepository = new EfRepository<Post>(context);
            var service = new CommentsService(commentsRepository, postsRepository);
            await this.SeedUsersAndPost(context);
            await this.SeedComments(context);

            // Act
            bool exists = await service.ExistsAsync(26); // Valid id.
            bool existsTwo = await service.ExistsAsync(5951); // Invalid id.

            // Assert
            Assert.True(exists);
            Assert.False(existsTwo);
        }

        private void InitilaizeMapper() => AutoMapperConfig.RegisterMappings(
                typeof(PostCommentViewModel).GetTypeInfo().Assembly,
                typeof(Comment).GetTypeInfo().Assembly);

        private async Task SeedUsersAndPost(ApplicationDbContext context)
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

        private async Task SeedComments(ApplicationDbContext context)
        {
            var comment = new Comment
            {
                Id = 26,
                CreatorId = "userId",
                PostId = 52,
                Text = "Comment Text",
            };

            var secondComment = new Comment
            {
                Id = 27,
                CreatorId = "userTwoId",
                PostId = 52,
                Text = "Comment Text 2",
            };

            var thirdComment = new Comment
            {
                Id = 28,
                CreatorId = "userThreeId",
                PostId = 52,
                Text = "Comment Text 3",
            };

            // Adding them separately so the order doesn't change.
            await context.Comments.AddAsync(comment);
            await context.SaveChangesAsync();
            await context.Comments.AddAsync(secondComment);
            await context.SaveChangesAsync();
            await context.Comments.AddAsync(thirdComment);
            await context.SaveChangesAsync();
        }
    }
}
