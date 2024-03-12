using Banlab.Social.Api.Data;
using Newtonsoft.Json;

namespace Banlab.Social.Api.Domain
{
    public class Comment: BaseEntity
    {
        public Comment(string postId, string content, string creatorId, string userId)
        {
            ArgumentNullException.ThrowIfNull(postId);
            ArgumentNullException.ThrowIfNull(content);
            ArgumentNullException.ThrowIfNull(userId);
            ArgumentNullException.ThrowIfNull(creatorId);

            Id = $"{Ulid.NewUlid()}-{userId}";
            PostId = postId;
            Content = content;
            CreatorId = creatorId;
            CreatedAt = DateTime.UtcNow;
        }

        public string Content { get; set; }

        public string PostId { get; set; }

        [JsonProperty("ttl", NullValueHandling = NullValueHandling.Ignore)]
        public int? Ttl { get; set; }

        public string CreatorId { get; set; }

        public string UserId { get; set; }

        public DateTime CreatedAt { get; private set; }

        public bool IsDeleted => Ttl.HasValue;

        public void Delete() => Ttl = 30;
    }
}
