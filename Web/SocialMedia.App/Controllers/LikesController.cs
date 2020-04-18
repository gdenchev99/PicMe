using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.Services.Data;
using SocialMedia.Web.ViewModels;
using SocialMedia.Web.ViewModels.Likes;
using System.Threading.Tasks;

namespace SocialMedia.App.Controllers
{
    [Authorize]
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

            bool likeExists = await this.service.IsPostLikedByUserAsync(model.UserId, model.PostId);

            if (likeExists)
            {
                return BadRequest(new BadRequestViewModel { Message = "You have already liked this post." });
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

            bool likeExists = await this.service.IsPostLikedByUserAsync(model.UserId, model.PostId);

            if (!likeExists)
            {
                return BadRequest(new BadRequestViewModel { Message = "You haven't liked this post." });
            }

            var result = await this.service.RemoveAsync(model);

            return Ok(result);
        }

        [HttpGet("Liked")]
        public async Task<bool> IsLiked(string userId, int postId)
        {
            var result = await this.service.IsPostLikedByUserAsync(userId, postId);

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
