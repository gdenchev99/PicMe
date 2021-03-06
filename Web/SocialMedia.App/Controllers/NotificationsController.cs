﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.Services.Data;
using System.Threading.Tasks;

namespace SocialMedia.App.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class NotificationsController : ControllerBase
    {
        private readonly INotificationsService service;

        public NotificationsController(INotificationsService service)
        {
            this.service = service;
        }

        [HttpGet("Get")]
        public async Task<IActionResult> GetNotifications(string userId, int skipCount, int takeCount)
        {
            var result = await this.service.GetNotificationsAsync(userId, skipCount, takeCount);

            return Ok(result);
        }

        [HttpGet("Unread")]
        public async Task<IActionResult> GetUnreadNotifications(string userId)
        {
            var result = await this.service.GetUnreadNotificationsCountAsync(userId);

            return Ok(result);
        }

        [HttpPost("UpdateStatus")]
        public async Task<IActionResult> UpdateReadStatus(string userId)
        {
            await this.service.UpdateReadStatusAsync(userId);

            return Ok();
        }
    }
}
