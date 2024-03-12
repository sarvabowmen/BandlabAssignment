using Banlab.Social.Api.ViewModels;

namespace Banlab.Social.Api.Services.Interfaces
{
    public interface IPostService
    {
        Task<string> CreatePost(CreatePostViewModel post);
    }
}
