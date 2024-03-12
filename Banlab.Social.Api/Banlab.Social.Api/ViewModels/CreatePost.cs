using System.ComponentModel.DataAnnotations;

namespace Banlab.Social.Api.ViewModels
{
    public class CreatePost
    {
        [Required]
        public required string Caption { get; set; }
        public required string OriginalImageUrl { get; set; }
        public required string ResizedImageUrl { get; set; }
        public required string UserId { get; set; }
        public required string Creator { get; set; }
    }
}
