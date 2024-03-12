using Banlab.Social.Api.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Banlab.Social.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        [HttpPost("{postId:string}")]
        public IActionResult Add(CreatePostViewModel updatePostViewModel, string postId)
        {
            return Ok();
        }

        [HttpDelete("{commentId:string}")]
        public IActionResult Delete(CreatePostViewModel updatePostViewModel, string postId)
        {
            return Ok();
        }
    }
}
