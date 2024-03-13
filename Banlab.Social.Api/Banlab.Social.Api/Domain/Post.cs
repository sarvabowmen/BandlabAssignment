using Banlab.Social.Api.Data;
using Microsoft.Extensions.Hosting;
using System.Xml.Linq;
using static Azure.Core.HttpHeader;

namespace Banlab.Social.Api.Domain
{
    public class Post : BaseEntity
    {

        private List<Comment> _comments;
        public Post() {
            _comments = [];
        }
        public Post(string creatorId, string userId, string imageUrl, List<Comment>? comments = null)
        {
            ArgumentNullException.ThrowIfNull(creatorId);
            ArgumentNullException.ThrowIfNull(imageUrl);
            ArgumentNullException.ThrowIfNull(userId);

            Id = Id = $"{Ulid.NewUlid()}-{userId}";
            CreatorId = creatorId;
            UserId = userId;
            CreatedAt = DateTime.UtcNow;
            _comments = comments ?? [];
        }
        public string Caption { get; set; }

        public int CommentsCount { get; set; }
        public string ImageUrl { get; set; }

        public string CreatorId { get; set; }


        public string UserId { get; set; }

        public DateTime CreatedAt { get; set; }

        public IReadOnlyList<Comment> RecentComments { get => _comments.AsReadOnly(); }
        public void IncreamentComment() => CommentsCount++;

        public bool HasComment(Comment comment)
        {
           var findComments = _comments.Find(comment => comment.Id == Id);
            return findComments != null;
        }
        public void AddToRecentCommentList(Comment comment)
        {
            if (RecentComments.Count < 2)
                _comments.Append(comment);
            else
            {
                var orderedComments = _comments.OrderBy(e => CreatedAt).ToList();
                orderedComments[0] = comment;

                _comments = orderedComments;
            }
        }

        public bool RemoveFromRecentComments(Comment comment)
        {
            return _comments.Remove(comment);
        }
    }
}
