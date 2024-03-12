using System.ComponentModel.DataAnnotations;

namespace Banlab.Social.Api.ViewModels
{
    public class CreatePostViewModel
    {
        [Required]
        public required string Caption { get; set; }
        public required string ImageUrl { get; set; }
        public required string UserId { get; set; }
        public required string CreatorId { get; set; }
    }
}
