using AutoMapper;
using Banlab.Social.Api.Data.Repository;
using Banlab.Social.Api.Domain;
using Banlab.Social.Api.Services.Interfaces;
using Banlab.Social.Api.ViewModels;
using Microsoft.Azure.Cosmos;

namespace Banlab.Social.Api.Services
{

    public class PostService(PostRepository postsRepository, CommentRepository commentsRepository, ILogger<PostService> logger, IMapper mapper) : IPostService
    {
        private readonly PostRepository _postsRepository = postsRepository;
        private readonly CommentRepository _commentsRepository = commentsRepository;
        private readonly ILogger<PostService> _logger = logger;
        private readonly IMapper _mapper = mapper;

        public async Task<string> CreatePost(CreatePostViewModel post)
        {
            var postDto = _mapper.Map<CreatePostViewModel, Post>(post);
            await _postsRepository.UpsertAsync(postDto);
            return postDto.Id;
        }

        public async Task UpdatePostWithRecentComments(Comment comment)
        {
            var post = await _postsRepository.GetById(comment.PostId);
            if (post is null)
                _logger.LogError("Related post not found. Post Id: {PostId}", comment.PostId);


            post.AddToRecentCommentList(comment);
            post.CommentsCount++;
            await _postsRepository.UpsertAsync(post);
        }

        public async Task CascadeCommentDeletiontoPost(Comment comment)
        {
            var post = await _postsRepository.GetById(comment.PostId);
            if (post is null)
                _logger.LogError("Post not found while processing comments change feed. Post Id:{PostId}", comment.PostId);

            post.CommentsCount--;

            var isCommentRemovedFromRecentComments = post.RemoveFromRecentComments(comment);
            if (isCommentRemovedFromRecentComments && post.RecentComments.Any())
            {
                var latestCommentSince = await _commentsRepository.GetTop2CommentsByPostIdAndDate(post.Id, post.RecentComments[0].CreatedAt);
                if (latestCommentSince is not null)
                {
                    foreach (var item in latestCommentSince)
                    {
                        if(!post.HasComment(item))
                        {
                            post.AddToRecentCommentList(item);
                            break;
                        }
                       
                    }
                    
                }
            }

            await _postsRepository.UpsertAsync(post);
        }
    }
}
