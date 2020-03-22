﻿using Microsoft.AspNetCore.Mvc;
using SocialMedia.Services.Data;
using SocialMedia.Web.ViewModels.Posts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SocialMedia.App.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class PostsController : ControllerBase
    {
        private readonly IPostsService service;

        public PostsController(IPostsService service)
        {
            this.service = service;
        }

        [HttpGet("Get")]
        public async Task<IActionResult> Get(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var result = await this.service.GetAsync(id);

            return Ok(result);
        }

        [HttpGet("All")]
        public async Task<IActionResult> GetAll(string id)
        {
            var result = await this.service.GetAllAsync(id);

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

            var result = await this.service.CreateAsync(model);

            if (result == false)
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}
