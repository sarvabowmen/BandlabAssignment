using System;
using System.Collections.Generic;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Banlab.Social.Api;
using Banlab.Social.Api.Domain;
using Newtonsoft.Json.Linq;
using System.Xml.Linq;
using Banlab.Social.Api.Services;
using Banlab.Social.Api.Services.Interfaces;

namespace Bandlab.Social.FunctionApp
{
    public class UpdateRecentCommentsToPost
    {
        private readonly ILogger _logger;
        private readonly IPostService _postsService;

        public UpdateRecentCommentsToPost(ILoggerFactory loggerFactory, IPostService postsService)
        {
            _logger = loggerFactory.CreateLogger<UpdateRecentCommentsToPost>();
            _postsService = postsService;
        }

        [Function("UpdateRecentCommentsToPost")]
        public async Task Run([CosmosDBTrigger(
            databaseName: "Social",
            containerName: "comments",
            Connection = "CosmosDBConnection",
            LeaseContainerName = "postWithCommentLeases",
            StartFromBeginning = true,
            CreateLeaseContainerIfNotExists = true)] IReadOnlyList<Comment> input)
        {
            if (input != null && input.Count > 0)
            {
                try
                {
                    foreach (var comment in input)
                    {
                        if (!comment.IsDeleted)
                        {
                            await _postsService.UpdatePostWithRecentComments(comment);
                        }
                        else
                        {
                            await _postsService.CascadeCommentDeletiontoPost(comment);
                        }
                    }

                }
                catch (Exception)
                {

                    throw;
                }
                

            }
        }
    }
}
