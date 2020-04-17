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
            await this.SeedUserAndPost(context);
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

        private void InitilaizeMapper() => AutoMapperConfig.RegisterMappings(
                typeof(PostCommentViewModel).GetTypeInfo().Assembly,
                typeof(Comment).GetTypeInfo().Assembly);

        private async Task SeedUserAndPost(ApplicationDbContext context)
        {
            var user = new ApplicationUser
            {
                Id = "userId",
                UserName = "userName",
            };

            var post = new Post
            {
                Id = 52,
                CreatorId = user.Id,
            };

            await context.Users.AddAsync(user);
            await context.Posts.AddAsync(post);
            await context.SaveChangesAsync();
        }
    }
}
