using Banlab.Social.Api.Domain;
using Banlab.Social.Api.ViewModels;

namespace Banlab.Social.Api.Services.Interfaces
{
    public interface IPostService
    {
        Task<string> CreatePost(CreatePostViewModel post);
        Task CascadeCommentDeletiontoPost(Comment comment);
        Task UpdatePostWithRecentComments(Comment comment);
    }
}
