using AutoMapper;
using Banlab.Social.Api.Domain;
using Banlab.Social.Api.ViewModels;

namespace Banlab.Social.Api.Mappers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Post, CreatePostViewModel>();
            CreateMap<CreatePostViewModel, Post>().ConstructUsing(x => new Post(x.CreatorId, x.UserId, x.ImageUrl, null));
            CreateMap<Comment, CreateCommentViewModel>();
            CreateMap<CreateCommentViewModel, Comment>().ConstructUsing(x => new Comment(x.PostId, x.Content, x.CreatorId, x.CreatorId));
        }
    }
}
