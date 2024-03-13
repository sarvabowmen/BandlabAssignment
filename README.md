# Architecture:
![Bandlab drawio](https://github.com/sarvabowmen/BandlabAssignment/assets/17758649/2d97ddef-832d-4319-b9f3-1e30be89fd64)

# Datamodel
  1. Choice of Database - No SQL
       - Need horizontal scalablity
       - Need flexiblity in storing unstructed data
       - High Write throughput for posts
  2. Data model
       - **posts** collection will have post caption, userId, postId image, recent comments (max 2), comment total count, Index is created for comment total count as we need to order by comment count for fetching cursor based paginated results.
       - partition key - CreatorId
       - **comments** collection will have commentId, content, userid, postId, createdDate. Indexes are created for createdDate
       - partition key - postId
### Overview:
  1. Client tries to create a post and drags and drop the image to the UI, Image is uploaded to the Image function which takes care of validating the size and processing and resizing the image and uploading the same to blob store.
  2. Client then calls the AddPost Function which get the Image url uploaded to blob, caption etc and inserts into the post collection.
  3. For adding comemnt client calls the Comment Add Function from CommentFunction and pass the postId as well along with comment details
  4. For deleting comments client calls the  Comment Delete Function from CommentFunction.
  5. There is a Cosmos Db Trigger function is configure to listen to the comments collection, which takes care of updating the posts collection with recent comments and total comment count, whenever a new comment is added against a post.


## Getting the app running
The application comprises of the two projects
- Banlab.Social.Api
- Bandlab.Social.FunctionApp

1. Open the solution in VS 2022 and make Banlab.Social.Api as startup Project and Run.
2. Open the solution in VS 2022 and make  Bandlab.Social.FunctionApp as startup Project and Run.
3. Now both the Api and function app will be running parallely
4. 

## Infrastructure Needs to be created
- Azure Blob Storage account
- Azure Blob Storage container
- Cosmos Database: A cosmos db database with two containers:
  posts container with CreatorId as the partitionKey
  comments container with postId as the partitionKey, and ttl enabled without a default duration.
## Environment Variables to updated in appsettings.json
  "CosmosDb": {
    "DatabaseName": "Social",
    "ConnectionString": "<COSMOSDB CONNECTIONSTRING>"
  },
   "BlobStorageConfig": {
   "ConnectionString": "<STORAGE ACCOUNT CONNECTIONSTRING>",
   "Container": "image"
 }
