using AutoMapper;
using Banlab.Social.Api.Data.Repository;
using Banlab.Social.Api.Domain;
using Banlab.Social.Api.Services.Interfaces;
using Banlab.Social.Api.ViewModels;
using Microsoft.Azure.Cosmos;

namespace Banlab.Social.Api.Services
{

    public class PostService(PostRepository postsRepository, ILogger<PostService> logger, IMapper mapper) : IPostService
    {
        private readonly PostRepository _postsRepository = postsRepository;
        private readonly ILogger<PostService> _logger = logger;
        private readonly IMapper _mapper = mapper;

        public async Task<string> CreatePost(CreatePostViewModel post)
        {
            var postDto = _mapper.Map<CreatePostViewModel, Post>(post);
            await _postsRepository.UpsertAsync(postDto);
            return postDto.Id;
        }
    }
}
