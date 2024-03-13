using Banlab.Social.Api.Domain;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Options;

namespace Banlab.Social.Api.Data.Repository
{
    public class PostRepository : BaseRepository<Post>
    {
        public static string PostsContainer = "posts";
        public PostRepository(CosmosClient cosmosClient, IOptions<CosmosDbOptions> cosmosSettings)
            : base(cosmosClient, cosmosSettings, PostsContainer)
        {
        }

        public async Task<List<Post>> GetAllPostsByUser(string userId,int offset, int limit)
        {
            var parameterizedQuery = new QueryDefinition(query: "SELECT * FROM posts WHERE posts.userId = @userId ORDER BY posts.commentCount OFFSET 0 LIMIT 3").WithParameter("@userId", userId).WithParameter("@offset", offset - 1).WithParameter("@limit", limit);

            using FeedIterator<Post> filteredFeed = _container.GetItemQueryIterator<Post>(queryDefinition: parameterizedQuery);
            var postList = new List<Post>();
            while (filteredFeed.HasMoreResults)
            {
                FeedResponse<Post> response = await filteredFeed.ReadNextAsync();
                foreach (Post item in response)
                {
                    postList.Add(item);
                }
            }

            return postList;
        }
    }
}
