using Microsoft.AspNetCore.Mvc;
using SocialMedia.Services.Data;
using SocialMedia.Web.ViewModels.Messages;
using System.Threading.Tasks;

namespace SocialMedia.App.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class MessagesController : ControllerBase
    {
        private readonly IMessagesService service;

        public MessagesController(IMessagesService service)
        {
            this.service = service;
        }

        [HttpGet("ChatRooms")]
        public async Task<IActionResult> GetChatRooms(string userId)
        {
            var result = await this.service.GetAllChatRoomsAsync(userId);

            return Ok(result);
        }

        [HttpPost("Send")]
        public async Task<IActionResult> SendMessage([FromBody]MessageInputModel model)
        {
            await this.service.CreateAsync(model);

            return Ok();
        }

        [HttpGet("ChatRoom")]
        public async Task<IActionResult> GetChatRoom(string username)
        {
            var result = await this.service.GetChatRoom(username);

            return Ok(result);
        }
    }
}
