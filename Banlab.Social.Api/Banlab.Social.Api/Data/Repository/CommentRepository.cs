using Banlab.Social.Api.Domain;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;
using Microsoft.Extensions.Options;
using System.ComponentModel;

namespace Banlab.Social.Api.Data.Repository
{
    public class CommentRepository : BaseRepository<Comment>
    {
        public static string CommentsContainer = "comments";
        public CommentRepository(CosmosClient cosmosClient, IOptions<CosmosDbOptions> cosmosSettings)
       : base(cosmosClient, cosmosSettings, CommentsContainer)
        {
           
        }

        public async Task AddCommentAsync(Comment item) => await _container.UpsertItemAsync(item, new(item.PostId));

        public async Task<List<Comment>> GetTop2CommentsByPostIdAndDate(string postId, DateTime createdAt)
        {
            var parameterizedQuery = new QueryDefinition(query: "SELECT TOP 2 * FROM comments WHERE comments.postId = @postId and comments.createdAt > @createdAt ORDER BY comments.createdAt DESC").WithParameter("@postId", postId).WithParameter("@createdAt", createdAt);

            using FeedIterator<Comment> filteredFeed = _container.GetItemQueryIterator<Comment>(queryDefinition: parameterizedQuery);
            var commentList = new List<Comment>();
            while (filteredFeed.HasMoreResults)
            {
                FeedResponse<Comment> response = await filteredFeed.ReadNextAsync();
                foreach (Comment item in response)
                {
                    commentList.Add(item);
                }
            }

            return commentList;
        }

    }
}
