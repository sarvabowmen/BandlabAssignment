using Banlab.Social.Api.Data;
using System.Xml.Linq;
using static Azure.Core.HttpHeader;

namespace Banlab.Social.Api.Domain
{
    public class Post : BaseEntity
    {
        public Post() { 
        
        }
        public Post(string creatorId, string userId, string imageUrl)
        {
            ArgumentNullException.ThrowIfNull(creatorId);
            ArgumentNullException.ThrowIfNull(imageUrl);
            ArgumentNullException.ThrowIfNull(userId);

            Id = Id = $"{Ulid.NewUlid()}-{userId}";
            CreatorId = creatorId;
            UserId = userId;
            CreatedAt = DateTime.UtcNow;
        }
        public string Caption { get; set; }

        public int CommentsCount { get; set; }
        public string ImageUrl { get; set; }

        public string CreatorId { get; set; }

        public string UserId { get; set; }

        public DateTime CreatedAt { get; private set; }
    }
}
