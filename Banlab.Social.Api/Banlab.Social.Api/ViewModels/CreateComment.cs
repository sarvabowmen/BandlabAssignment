using System.ComponentModel.DataAnnotations;

namespace Banlab.Social.Api.ViewModels
{
    public class CreateCommentViewModel
    {
        public string Content { get; set; }
        public string CreatorId { get; set; }
    }
}
