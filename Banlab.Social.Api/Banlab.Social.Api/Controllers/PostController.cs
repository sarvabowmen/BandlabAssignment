using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Banlab.Social.Api.ViewModels;
using Banlab.Social.Api.Domain;
using Microsoft.AspNetCore.Http.HttpResults;
using Banlab.Social.Api.Services;
using Banlab.Social.Api.Services.Interfaces;

namespace Banlab.Social.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly ILogger<PostController> _logger;
        private readonly IPostService _postService;

        public PostController(ILogger<PostController> logger, IPostService postsService)
        {
            _logger = logger;
            _postService = postsService;
        }

        [HttpPost(Name = "Create")]
        public async Task<IActionResult> CreateAsync(CreatePostViewModel createPostViewModel)
        {
            if (string.IsNullOrEmpty(createPostViewModel.CreatorId))
                throw new ArgumentNullException("CreatorId is null or empty");

            if (string.IsNullOrEmpty(createPostViewModel.UserId))
                throw new ArgumentNullException("UserId is null or empty");

            if (string.IsNullOrEmpty(createPostViewModel.ImageUrl))
                throw new ArgumentNullException("ImageUrl is null or empty");


            var postId = await _postService.CreatePost(createPostViewModel);

            return new OkObjectResult(postId);
        }

        [HttpGet("{userId}/{pageNumber:int?}/{pageSize:int?}")]
        public async Task<IActionResult> GetPostListByUser(int userId, int pageNumber = 1, int pageSize = 10)
        {
            return Ok();
        }
    }
}

