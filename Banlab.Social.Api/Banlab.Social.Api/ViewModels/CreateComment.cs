using System.ComponentModel.DataAnnotations;

namespace Banlab.Social.Api.ViewModels
{
    public class CreateCommentViewModel
    {
        [Required]
        public required string Content { get; set; }

        [Required]
        public required string CreatorId { get; set; }

        [Required]
        public required string PostId { get; set; }
    }
}
