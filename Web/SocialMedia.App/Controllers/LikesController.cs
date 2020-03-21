using Microsoft.AspNetCore.Mvc;
using SocialMedia.Services.Data;
using SocialMedia.Web.ViewModels.Likes;
using System.Threading.Tasks;

namespace SocialMedia.App.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class LikesController : ControllerBase
    {
        private readonly ILikesService service;

        public LikesController(ILikesService service)
        {
            this.service = service;
        }

        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody] AddLikeModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Model state is not valid.");
            }

            var result = await this.service.AddAsync(model);

            return Ok(result);
        }

        [HttpPost("Remove")]
        public async Task<IActionResult> Remove([FromBody] AddLikeModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Model state is not valid");
            }

            var result = await this.service.RemoveAsync(model);

            return Ok(result);
        }

        [HttpGet("Liked")]
        public bool IsLiked(string userId, int postId)
        {
            var result = this.service.IsPostLikedByUser(userId, postId);

            return result;
        }

        [HttpGet("Feed")]
        public async Task<IActionResult> GetLastThree(int postId)
        {
            var result = await this.service.GetLastThreeAsync(postId);

            if (result == null)
            {
                return BadRequest();
            }

            return Ok(result);
        }
    }
}
