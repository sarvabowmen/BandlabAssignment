using Microsoft.Azure.Cosmos.Fluent;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Banlab.Social.Api.Configuration;
using Banlab.Social.Api.Mappers;
using Banlab.Social.Api.Data.Repository;
using Banlab.Social.Api.Repositories;
using Banlab.Social.Api.Services.Interfaces;
using Banlab.Social.Api.Services;
using Azure.Storage.Blobs;
using Bandlab.Social.FunctionApp;

namespace Banlab.Social.Api
{
    public static class StartupExtensions
    {
        public static IServiceCollection AddConfig(
             this IServiceCollection services, IConfiguration config)
        {
           

            return services;
        }

        public static IServiceCollection AddMyDependencyGroup(
           this IServiceCollection services, IConfiguration config)
        {
            services.AddAutoMapper(typeof(AutoMapperProfile));
            ConfigureCosmosDb(services, config);
            ConfigureBlobStorage(services, config);

            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped<PostRepository>();
            services.AddScoped<CommentRepository>();
            services.AddScoped<IPostService, PostService>();
            services.AddScoped<ICommentService, CommentService>();
            services.AddScoped<IImageService, ImageService>();

            return services;
        }
        private static void ConfigureCosmosDb(IServiceCollection services, IConfiguration configuration)
        {
            var cosmosConfig = configuration.GetSection("CosmosDb").Get<CosmosDbOptions>();
            var cosmosClient = new CosmosClientBuilder(cosmosConfig.ConnectionString)
                .WithContentResponseOnWrite(false)
                .WithSerializerOptions(new CosmosSerializationOptions { PropertyNamingPolicy = CosmosPropertyNamingPolicy.CamelCase })
                .Build();


            services.Configure<CosmosDbOptions>(configuration.GetSection("CosmosDb"));

            services.AddSingleton(cosmosClient);
        }

        private static void ConfigureBlobStorage(IServiceCollection services, IConfiguration configuration)
        {
            var blobStorageSettings = configuration.GetSection(nameof(BlobStorageConfig)).Get<BlobStorageConfig>();
            ArgumentNullException.ThrowIfNull(blobStorageSettings);

            var blobServiceClient = new BlobServiceClient(blobStorageSettings.ConnectionString);
            services.AddSingleton(blobServiceClient.GetBlobContainerClient(blobStorageSettings.Container));
        }

    }
}
