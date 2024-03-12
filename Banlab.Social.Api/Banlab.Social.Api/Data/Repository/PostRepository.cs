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
    }
}
