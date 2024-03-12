using AutoMapper;
using Banlab.Social.Api.Data.Repository;
using Banlab.Social.Api.Domain;
using Banlab.Social.Api.Services.Interfaces;
using Banlab.Social.Api.ViewModels;
using Microsoft.Azure.Cosmos;

namespace Banlab.Social.Api.Services
{

    public class CommentService(CommentRepository commentRepository, ILogger<CommentService> logger, IMapper mapper) : ICommentService
    {
        private readonly CommentRepository _commentsRepository = commentRepository;
        private readonly ILogger<CommentService> _logger = logger;
        private readonly IMapper _mapper = mapper;

        public async Task<string> AddCommentToPost(string postId, CreateCommentViewModel comment)
        {
            var commentDto = _mapper.Map<CreateCommentViewModel, Comment>(comment);
            await _commentsRepository.AddCommentAsync(commentDto);
            return commentDto.Id;
        }

        public async Task DeleteComment(string commentId)
        {
            var comment = await _commentsRepository.GetById(commentId);
            comment.Delete();
            await _commentsRepository.AddCommentAsync(comment);
        }
    }
}
