using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialMedia.Data.Models;
using SocialMedia.Services;
using SocialMedia.Services.Data;
using SocialMedia.Web.ViewModels;
using SocialMedia.Web.ViewModels.Posts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SocialMedia.App.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class PostsController : ControllerBase
    {
        private readonly IPostsService service;
        private readonly IProfilesService profilesService;
        private readonly ICloudinaryService cloudinaryService;

        public PostsController(
            IPostsService service,
            IProfilesService profilesService,
            ICloudinaryService cloudinaryService)
        {
            this.service = service;
            this.profilesService = profilesService;
            this.cloudinaryService = cloudinaryService;
        }

        [HttpGet("Get")]
        public async Task<IActionResult> Get(int id)
        {
            bool postExists = await this.service.ExistsAsync(id);

            if (!postExists)
            {
                return BadRequest(new BadRequestViewModel { Message = "This post does not exist." });
            }

            var result = await this.service.GetAsync(id);

            return Ok(result);
        }

        [HttpGet("Feed")]
        public async Task<IActionResult> GetFeed(string id, int skipCount, int takeCount)
        {
            var result = await this.service.GetFeedAsync(id, skipCount, takeCount);

            return Ok(result);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromForm] PostCreateModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.ToDictionary(
                         kvp => kvp.Key,
                         kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                ));
            }

            // Upload file to Cloudinary and get the url to the media and the public id of the media.
            var fileUpload = await this.cloudinaryService.UploadFileAsync(model.MediaSource, model.CreatorId);
            var mediaUrl = fileUpload.SecureUri.ToString();
            var publicId = fileUpload.PublicId;

            var result = await this.service.CreateAsync(model, mediaUrl, publicId);

            if (result == false)
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpPost("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            bool postExists = await this.service.ExistsAsync(id);

            if (!postExists)
            {
                return BadRequest(new BadRequestViewModel { Message = "This post does not exist." });
            }

            await this.service.DeleteAsync(id);

            return Ok();
        }

        [HttpGet("Profile")]
        public async Task<IActionResult> Profile(string username)
        {
            bool userExists = await this.profilesService.UserExistsByNameAsync(username);

            if (!userExists)
            {
                return BadRequest(new BadRequestViewModel { Message = "This user does not exist." });
            }

            var result = await this.service.GetProfilePostsAsync(username);

            return Ok(result);
        }
    }
}
