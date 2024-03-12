using Banlab.Social.Api.ViewModels;

namespace Banlab.Social.Api.Services.Interfaces
{
    public interface ICommentService
    {
        Task<string> AddCommentToPost(string postId, CreateCommentViewModel comment);
        Task DeleteComment(string commentId);
    }
}
