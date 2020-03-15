using Microsoft.AspNetCore.Mvc;
using SocialMedia.Services.Data;
using SocialMedia.Web.ViewModels.Comments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMedia.App.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentsService service;

        public CommentsController(ICommentsService service)
        {
            this.service = service;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] CommentCreateModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.ToDictionary(
                         kvp => kvp.Key,
                         kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                ));
            }

            var isAdded = await this.service.CreateAsync(model);
            
            if (isAdded == false)
            {
                return BadRequest("Failed to add to the database.");
            }

            return Ok();
        }
    }
}
