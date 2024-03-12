using Banlab.Social.Api.Domain;
using Banlab.Social.Api.Services.Interfaces;
using Banlab.Social.Api.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Azure.Cosmos;
using System.Net;
using System.Threading;

namespace Banlab.Social.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ILogger<PostController> _logger;
        private readonly ICommentService _commentService;


        public CommentController(ILogger<PostController> logger, ICommentService commentService)
        {
            _logger = logger;
            _commentService = commentService;
        }
        [HttpPost("post/{postId}")]
        public async Task<IActionResult> AddCommentAsync(CreateCommentViewModel createCommentViewModel, string postId)
        {
            try
            {
                if (string.IsNullOrEmpty(createCommentViewModel.CreatorId))
                    throw new ArgumentNullException("CreatorId is null or empty");

                if (string.IsNullOrEmpty(createCommentViewModel.Content))
                    throw new ArgumentNullException("Content is null or empty");


                var commentId = await _commentService.AddCommentToPost(postId, createCommentViewModel);

                return new OkObjectResult(commentId);

            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex);
            }
            catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                return new NotFoundResult();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Something went wrong while processing this request");
                return new ConflictResult();
            }

        }

        [HttpDelete("{commentId}")]
        public async Task<IActionResult> DeleteAsync(string commentId)
        {

            try
            {
                if (string.IsNullOrEmpty(commentId))
                    throw new ArgumentNullException("CommentId is null or empty");


                await _commentService.DeleteComment(commentId);

                return Ok();

            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex);
            }
            catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                return new NotFoundResult();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Something went wrong while processing this request");
                return new ConflictResult();
            }

        }
    }
}
