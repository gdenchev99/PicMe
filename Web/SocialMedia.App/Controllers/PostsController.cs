using Microsoft.AspNetCore.Mvc;
using SocialMedia.Web.ViewModels.Posts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMedia.App.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class PostsController : ControllerBase
    {
        public PostsController()
        {

        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] PostInputModel model)
        {

            return Ok();
        }
    }
}
