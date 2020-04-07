﻿using Microsoft.AspNetCore.Mvc;
using SocialMedia.Services.Data;
using SocialMedia.Web.ViewModels.Profiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMedia.App.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class ProfilesController : ControllerBase
    {
        private readonly IProfilesService service;

        public ProfilesController(IProfilesService service)
        {
            this.service = service;
        }
        
        [HttpGet("Get")]
        public async Task<IActionResult> Get(string username)
        {
            var userProfile = await this.service.GetUserProfileAsync(username);

            return Ok(userProfile);
        }

        [HttpPost("Follow")]
        public async Task<IActionResult> Follow([FromBody]AddFollowerModel model)
        {
            // Follow the user
            await this.service.AddFollowerAsync(model);

            return Ok();
        }

        [HttpPost("Unfollow")]
        public async Task<IActionResult> Unfollow([FromBody]AddFollowerModel model)
        {
            var result = await this.service.RemoveFollowerAsync(model);

            return Ok(result);
        }

        [HttpGet("Followers")]
        public async Task<IActionResult> Followers(string username)
        {
            var result = await this.service.GetUserFollowersAsync(username);

            return Ok(result);
        }

        [HttpGet("Followings")]
        public async Task<IActionResult> Followings(string username)
        {
            var result = await this.service.GetUserFollowingsAsync(username);

            return Ok(result);
        }

        [HttpPost("ProfilePicture")]
        public async Task<IActionResult> ProfilePicture([FromForm]UploadPictureInputModel model)
        {
            var result = await this.service.UploadProfilePicture(model);

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
