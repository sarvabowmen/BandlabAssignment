namespace Banlab.Social.Api
{

    public class CosmosDbOptions
    {
        public required string DatabaseName { get; set; }
        public required string ConnectionString { get; set; }

        
        public static string CommentsContainer = "comments";
    }
    
}
