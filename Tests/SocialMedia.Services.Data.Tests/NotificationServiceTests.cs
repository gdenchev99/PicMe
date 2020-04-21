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
    using SocialMedia.Web.ViewModels.Notifications;
    using Xunit;

    public class NotificationServiceTests
    {
        [Fact]
        public async Task CreateNotificationAsync_WithValidData_ShouldAddNotificationToDatabase()
        {
            // Arrange
            var context = InMemoryDbContext.Initiliaze();
            var notificationsRepository = new EfRepository<Notification>(context);
            var service = new NotificationsService(notificationsRepository);
            await this.SeedUsersAndPost(context);
            string info = "test info";

            // Act
            await service.CreateNotificationAsync("userId", 52, info);
            string expectedInfo = info;
            string actualInfo = context.Notifications.FirstOrDefault().Info;

            // Assert
            Assert.Equal(expectedInfo, actualInfo);
        }

        [Fact]
        public async Task GetNotificationsAsync_WithValidUserId_ShouldReturnAllUserNotifications()
        {
            // Arrange
            this.InitilaizeMapper();
            var context = InMemoryDbContext.Initiliaze();
            var notificationsRepository = new EfRepository<Notification>(context);
            var service = new NotificationsService(notificationsRepository);
            await this.SeedUsersAndPost(context);
            await this.SeedNotifications(context);
            int takeCount = 2;

            // Act
            var notifications = await service.GetNotificationsAsync("userId", 0, takeCount);
            bool notificationOneExists = context.Notifications.Any(x => x.Id == 26); // Valid id
            bool notificationTwoExists = context.Notifications.Any(x => x.Id == 27); // Valid id
            bool notificationThreeExists = context.Notifications.Any(x => x.Id == 257); // Invalid id

            // Assert
            Assert.True(notificationOneExists);
            Assert.True(notificationTwoExists);
            Assert.False(notificationThreeExists);
        }

        [Fact]
        public async Task GetUnreadNotificationsAsync_WithValidUserId_ShouldReturnUnreadNotificationsCount()
        {
            // Arrange
            var context = InMemoryDbContext.Initiliaze();
            var notificationsRepository = new EfRepository<Notification>(context);
            var service = new NotificationsService(notificationsRepository);
            await this.SeedUsersAndPost(context);
            await this.SeedNotifications(context);

            // Act
            int expectedResult = context.Notifications.Where(x => x.UserId == "userId" && x.Read == false).Count();
            int actualResult = await service.GetUnreadNotificationsCountAsync("userId");

            // Assert
            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public async Task UpdateReadStatusAsync_WithValidUserId_ShouldUpdateDatabase()
        {
            // Arrange
            var context = InMemoryDbContext.Initiliaze();
            var notificationsRepository = new EfRepository<Notification>(context);
            var service = new NotificationsService(notificationsRepository);
            await this.SeedUsersAndPost(context);
            await this.SeedNotifications(context);

            // Act
            int expectedUnreadCount = 0;
            await service.UpdateReadStatusAsync("userId");

            // The count of unread notifications after we call the update method, should be 0.
            int actualUnreadCount = context.Notifications.Where(x => x.UserId == "userId" && x.Read == false).Count(); 

            // Assert
            Assert.Equal(expectedUnreadCount, actualUnreadCount);
        }

        [Fact]
        public async Task UpdateReadStatusAsync_WithNoNewNotifications_ShouldReturnMessage()
        {
            // Arrange
            var context = InMemoryDbContext.Initiliaze();
            var notificationsRepository = new EfRepository<Notification>(context);
            var service = new NotificationsService(notificationsRepository);
            await this.SeedUsersAndPost(context);

            // Act
            string expectedResult = "There are no new notifications";
            string actualResult = await service.UpdateReadStatusAsync("userId");

            // Assert
            Assert.Equal(expectedResult, actualResult);
        }

        private void InitilaizeMapper() => AutoMapperConfig.RegisterMappings(
                typeof(NotificationViewModel).GetTypeInfo().Assembly,
                typeof(Notification).GetTypeInfo().Assembly);

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

        private async Task SeedNotifications(ApplicationDbContext context)
        {
            var notification = new Notification
            {
                Id = 26,
                UserId = "userId",
                PostId = 52,
                Info = "Test Info One",
                Read = false,
            };

            var notificationTwo = new Notification
            {
                Id = 27,
                UserId = "userId",
                PostId = 52,
                Info = "Test Info Two",
                Read = true,
            };

            await context.AddRangeAsync(notification, notificationTwo);
            await context.SaveChangesAsync();
        }
    }
}
