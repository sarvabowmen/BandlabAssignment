using Banlab.Social.Api.Repositories;
using Banlab.Social.Api.Helpers;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Options;

namespace Banlab.Social.Api.Data.Repository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        protected readonly Container _container;

        public BaseRepository(CosmosClient cosmosClient, IOptions<CosmosDbOptions> options, string containerName)
            => _container = cosmosClient.GetContainer(options.Value.DatabaseName, containerName);

        public Task UpsertAsync(T item)
            => _container.UpsertItemAsync(item, new(item.Id.GetPartitionKey()));
    }
}
