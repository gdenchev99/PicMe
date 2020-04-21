using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.Common;
using SocialMedia.Data.Models;
using SocialMedia.Services.Data;
using SocialMedia.Web.ViewModels;
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
        public async Task<IActionResult> Ban(string id)
        {
            bool userExists = await this.profilesService.UserExistsByIdAsync(id);

            if (!userExists)
            {
                return BadRequest(new BadRequestViewModel { Message = "this user does not exist." });
            }

            bool result = await this.adminService.IsAdminAsync(id);

            if (!result)
            {
                return Forbid();
            }

            string endDate = await this.adminService.BanUserAsync(id);

            return Ok(endDate);
        }

        [HttpPost("Unban")]
        public async Task<IActionResult> Unban(string id)
        {
            bool userExists = await this.profilesService.UserExistsByIdAsync(id);

            if (!userExists)
            {
                return BadRequest(new BadRequestViewModel { Message = "this user does not exist." });
            }

            bool result = await this.adminService.IsAdminAsync(id);

            if (!result)
            {
                return Forbid();
            }

            await this.adminService.UnbanUserAsync(id);

            return Ok();
        }
    }
}
