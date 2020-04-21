using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.Services;
using SocialMedia.Services.Data;
using SocialMedia.Web.ViewModels;
using SocialMedia.Web.ViewModels.Profiles;
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
    public class ProfilesController : ControllerBase
    {
        private readonly IProfilesService service;
        private readonly ICloudinaryService cloudinaryService;

        public ProfilesController(
            IProfilesService service, 
            ICloudinaryService cloudinaryService)
        {
            this.service = service;
            this.cloudinaryService = cloudinaryService;
        }
        
        [HttpGet("Get")]
        public async Task<IActionResult> Get(string username)
        {
            bool userExists = await this.service.UserExistsByNameAsync(username);

            if (!userExists)
            {
                return NotFound();
            }

            var userProfile = await this.service.GetUserProfileAsync(username);

            return Ok(userProfile);
        }

        [HttpPost("Follow")]
        public async Task<IActionResult> Follow([FromBody]AddFollowerModel model)
        {
            bool followerExists = await this.service.FollowerExistsAsync(model.UserId, model.FollowerId);

            if (followerExists)
            {
                return BadRequest(new BadRequestViewModel { Message = "You are already following this user." });
            }

            // Follow the user
            await this.service.AddFollowerAsync(model);

            return Ok();
        }

        [HttpPost("Unfollow")]
        public async Task<IActionResult> Unfollow([FromBody]AddFollowerModel model)
        {
            bool followerExists = await this.service.FollowerExistsAsync(model.UserId, model.FollowerId);

            if (!followerExists)
            {
                return BadRequest(new BadRequestViewModel { Message = "You cannot unfollow this user, because you don't follow him." });
            }

            var result = await this.service.RemoveFollowerAsync(model);

            return Ok(result);
        }

        [HttpGet("Followers")]
        public async Task<IActionResult> Followers(string username)
        {
            bool userExists = await this.service.UserExistsByNameAsync(username);

            if (!userExists)
            {
                return NotFound();
            }

            var result = await this.service.GetUserFollowersAsync(username);

            return Ok(result);
        }

        [HttpGet("Followings")]
        public async Task<IActionResult> Followings(string username)
        {
            bool userExists = await this.service.UserExistsByNameAsync(username);

            if (!userExists)
            {
                return NotFound();
            }

            var result = await this.service.GetUserFollowingsAsync(username);

            return Ok(result);
        }

        [HttpGet("Requests")]
        public async Task<IActionResult> Requests(string id)
        {
            bool userExists = await this.service.UserExistsByIdAsync(id);

            if (!userExists)
            {
                return NotFound();
            }

            var result = await this.service.GetFollowerRequestsAsync(id);

            return Ok(result);
        }

        [HttpPost("Approve")]
        public async Task<IActionResult> ApproveRequest(string username)
        {
            bool userExists = await this.service.UserExistsByNameAsync(username);

            if (!userExists)
            {
                return NotFound();
            }

            var result = await this.service.ApproveRequestAsync(username);

            return Ok();
        }

        [HttpPost("Delete")]
        public async Task<IActionResult> DeleteRequest(string username)
        {
            bool userExists = await this.service.UserExistsByNameAsync(username);

            if (!userExists)
            {
                return NotFound();
            }

            var result = await this.service.DeleteRequestAsync(username);

            return Ok();
        }

        [HttpPost("ProfilePicture")]
        public async Task<IActionResult> ProfilePicture([FromForm]UploadPictureInputModel model)
        {
            var uploadResult = await this.cloudinaryService.UploadFileAsync(model.Picture, model.Id);

            var profilePictureUrl = uploadResult.SecureUri.ToString();
            var picturePublicId = uploadResult.PublicId;

            var result = await this.service.UploadProfilePictureAsync(model.Id, profilePictureUrl, picturePublicId);

            return Ok(result);
        }

        [HttpPost("Search")]
        public async Task<IActionResult> Search(string searchString)
        {
            var result = await this.service.SearchProfilesAsync(searchString);

            return Ok(result);
        }
    }
}
