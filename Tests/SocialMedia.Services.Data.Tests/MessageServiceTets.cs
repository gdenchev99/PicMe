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
    using SocialMedia.Web.ViewModels.Messages;
    using Xunit;

    public class MessageServiceTets
    {
        [Fact]
        public async Task GetAllChatRoomsAsync_WithValidUserId_ShouldReturnAllUserChatRooms()
        {
            // Arrange
            this.InitilaizeMapper();
            var context = InMemoryDbContext.Initiliaze();
            var messagesRepository = new EfRepository<Message>(context);
            var chatRoomsRepository = new EfRepository<ChatRoom>(context);
            var usersRepository = new EfRepository<ApplicationUser>(context);
            var service = new MessagesService(messagesRepository, chatRoomsRepository, usersRepository);
            await this.SeedUsers(context);
            await this.SeedChatRooms(context);

            // Act
            var rooms = await service.GetAllChatRoomsAsync("userId"); // Should return both rooms from the seeding.
            int expectedCount = context.ChatRooms.Count();
            int actualCount = rooms.Count();

            // Assert
            Assert.Equal(expectedCount, actualCount);
        }

        [Fact]
        public async Task CreateAsync_WithValidData_ShouldInitiliazeChatRoom()
        {
            // Arrange
            this.InitilaizeMapper();
            var context = InMemoryDbContext.Initiliaze();
            var messagesRepository = new EfRepository<Message>(context);
            var chatRoomsRepository = new EfRepository<ChatRoom>(context);
            var usersRepository = new EfRepository<ApplicationUser>(context);
            var service = new MessagesService(messagesRepository, chatRoomsRepository, usersRepository);
            await this.SeedUsers(context);

            var model = new MessageInputModel { UserOneId = "userId", UserTwoUsername = "userTwoName", Text = "test text" };

            // Act
            int expectedCount = context.ChatRooms.Count() + 1;
            await service.CreateAsync(model); // The chatroom does not exist, so sending the first message, should initiliaze it.
            int actualCount = context.ChatRooms.Count();

            // Assert
            Assert.Equal(expectedCount, actualCount);
        }

        [Fact]
        public async Task CreateAsync_WithValidData_ShouldAddMessageToDatabase()
        {
            // Arrange
            this.InitilaizeMapper();
            var context = InMemoryDbContext.Initiliaze();
            var messagesRepository = new EfRepository<Message>(context);
            var chatRoomsRepository = new EfRepository<ChatRoom>(context);
            var usersRepository = new EfRepository<ApplicationUser>(context);
            var service = new MessagesService(messagesRepository, chatRoomsRepository, usersRepository);
            await this.SeedUsers(context);
            await this.SeedChatRooms(context);

            var model = new MessageInputModel { UserOneId = "userId", UserTwoUsername = "userTwoName", Text = "test text" };

            // Act
            var message = await service.CreateAsync(model);
            string expectedMessageText = context.Messages.FirstOrDefault().Text;
            string actualMessageText = message.Text;

            // Assert
            Assert.Equal(expectedMessageText, actualMessageText);
        }

        [Fact]
        public async Task GetChatRoomMessagesAsync_WithValidData_ShouldReturnChatRoomMessages()
        {
            // Arrange
            this.InitilaizeMapper();
            var context = InMemoryDbContext.Initiliaze();
            var messagesRepository = new EfRepository<Message>(context);
            var chatRoomsRepository = new EfRepository<ChatRoom>(context);
            var usersRepository = new EfRepository<ApplicationUser>(context);
            var service = new MessagesService(messagesRepository, chatRoomsRepository, usersRepository);
            await this.SeedUsers(context);
            await this.SeedChatRooms(context);
            await context.Messages.AddAsync(new Message { ChatRoomId = 26, UserId = "userId", Text = "test text in room." });
            await context.SaveChangesAsync();

            // Act
            var messages = await service.GetChatRoomMessagesAsync("userId", "userTwoName");
            string expectedMessageText = context.Messages.FirstOrDefault().Text;
            string actualMessageText = messages.FirstOrDefault().Text;

            // Assert
            Assert.Equal(expectedMessageText, actualMessageText);
        }

        private void InitilaizeMapper() => AutoMapperConfig.RegisterMappings(
                typeof(AllChatRoomsViewModel).GetTypeInfo().Assembly,
                typeof(Message).GetTypeInfo().Assembly);

        private async Task SeedUsers(ApplicationDbContext context)
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

        private async Task SeedChatRooms(ApplicationDbContext context)
        {
            // Initiliazed by the user with id = "userId"
            var chatroom = new ChatRoom
            {
                Id = 26,
                UserOneId = "userId",
                UserTwoId = "userTwoId",
            };

            // Initiliazed by the user with id = "userThreeId"
            var chatroomTwo = new ChatRoom
            {
                Id = 27,
                UserOneId = "userThreeId",
                UserTwoId = "userId",
            };

            await context.ChatRooms.AddRangeAsync(chatroom, chatroomTwo);
            await context.SaveChangesAsync();
        }
    }
}
