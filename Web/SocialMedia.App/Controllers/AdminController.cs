using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.Common;
using SocialMedia.Data.Models;
using SocialMedia.Services.Data;
using SocialMedia.Web.ViewModels;
using SocialMedia.Web.ViewModels.Administration;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMedia.App.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService adminService;
        private readonly IProfilesService profilesService;

        public AdminController(IAdminService adminService, IProfilesService profilesService)
        {
            this.adminService = adminService;
            this.profilesService = profilesService;
        }

        [HttpGet("Get")]
        public async Task<IActionResult> Get(string id)
        {
            bool result = await this.adminService.IsAdminAsync(id);

            if (!result)
            {
                return Forbid();
            }

            var users = await this.adminService.GetUsersAsync();

            return Ok(users);
        }

        [HttpPost("Ban")]
        public async Task<IActionResult> Ban([FromBody]ActionInputModel model)
        {
            bool userExists = await this.profilesService.UserExistsByIdAsync(model.UserId);

            if (!userExists)
            {
                return BadRequest(new BadRequestViewModel { Message = "this user does not exist." });
            }

            bool result = await this.adminService.IsAdminAsync(model.AdminId);

            if (!result)
            {
                return Forbid();
            }

            string endDate = await this.adminService.BanUserAsync(model.UserId);

            return Ok(endDate);
        }

        [HttpPost("Unban")]
        public async Task<IActionResult> Unban([FromBody]ActionInputModel model)
        {
            bool userExists = await this.profilesService.UserExistsByIdAsync(model.UserId);

            if (!userExists)
            {
                return BadRequest(new BadRequestViewModel { Message = "this user does not exist." });
            }

            bool result = await this.adminService.IsAdminAsync(model.AdminId);

            if (!result)
            {
                return Forbid();
            }

            await this.adminService.UnbanUserAsync(model.UserId);

            return Ok();
        }
    }
}
