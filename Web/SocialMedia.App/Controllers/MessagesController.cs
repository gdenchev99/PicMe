using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.Services.Data;
using SocialMedia.Web.ViewModels;
using SocialMedia.Web.ViewModels.Messages;
using System.Threading.Tasks;

namespace SocialMedia.App.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class MessagesController : ControllerBase
    {
        private readonly IMessagesService service;
        private readonly IProfilesService profilesService;

        public MessagesController(IMessagesService service, IProfilesService profilesService)
        {
            this.service = service;
            this.profilesService = profilesService;
        }

        [HttpGet("ChatRooms")]
        public async Task<IActionResult> GetChatRooms(string userId)
        {
            bool userExists = await this.profilesService.UserExistsByIdAsync(userId);

            if (!userExists)
            {
                return NotFound();
            }

            var result = await this.service.GetAllChatRoomsAsync(userId);

            return Ok(result);
        }

        [HttpGet("ChatRoom")]
        public async Task<IActionResult> GetChatRoom(string currentId, string receiverUsername)
        {
            bool userExists = await this.profilesService.UserExistsByNameAsync(receiverUsername);

            if (!userExists)
            {
                return NotFound();
            }

            var result = await this.service.GetChatRoomMessagesAsync(currentId, receiverUsername);

            return Ok(result);
        }
    }
}
