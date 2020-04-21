using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.Services.Data;
using SocialMedia.Web.ViewModels;
using SocialMedia.Web.ViewModels.Comments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMedia.App.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentsService service;
        private readonly IPostsService postsService;

        public CommentsController(ICommentsService service, IPostsService postsService)
        {
            this.service = service;
            this.postsService = postsService;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody]CommentCreateModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.ToDictionary(
                         kvp => kvp.Key,
                         kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                ));
            }

            var postCreatorId = await this.service.CreateAsync(model);

            return Ok(postCreatorId);
        }

        [HttpPost("Delete")]
        public async Task<IActionResult> Delete([FromQuery]int id)
        {
            bool commentExists = await this.service.ExistsAsync(id);

            if (!commentExists)
            {
                return BadRequest(new BadRequestViewModel { Message = "Comment doesn't exist." });
            }

            await this.service.DeleteAsync(id);

            return Ok();
        }

        [HttpGet("All")]
        public async Task<IActionResult> GetPostComments(int postId, int skipCount, int takeCount)
        {
            bool postExists = await this.postsService.ExistsAsync(postId);

            if (!postExists)
            {
                return NotFound();
            }

            var result = await this.service.GetPostCommentsAsync(postId, skipCount, takeCount);

            return Ok(result);
        }

        [HttpGet("Feed")]
        public async Task<IActionResult> GetFeedComments(int postId)
        {
            bool postExists = await this.postsService.ExistsAsync(postId);

            if (!postExists)
            {
                return NotFound();
            }

            var result = await this.service.GetLastTwoAsync(postId);

            return Ok(result);
        }
    }
}
