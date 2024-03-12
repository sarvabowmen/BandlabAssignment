using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Banlab.Social.Api.ViewModels;
using Banlab.Social.Api.Domain;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Banlab.Social.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly ILogger<PostController> _logger;

        public PostController(ILogger<PostController> logger)
        {
            _logger = logger;
        }

        [HttpPost(Name = "Create")]
        public IActionResult Create(CreatePost createPostViewModel)
        {
            return Ok();
        }
    }
}
