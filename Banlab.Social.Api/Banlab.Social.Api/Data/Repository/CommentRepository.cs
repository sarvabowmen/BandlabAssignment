using Banlab.Social.Api.Domain;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Options;

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
        
    }
}
