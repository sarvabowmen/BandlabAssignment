namespace Banlab.Social.Api.Domain
{
    public class Post
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }
        public string Caption { get; set; }
        public string Creator { get; set; }
        public string UserId { get; set; }

        public DateTime CreatedAt { get; private set; }
    }
}
