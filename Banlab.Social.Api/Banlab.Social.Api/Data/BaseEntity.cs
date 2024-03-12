using System.Text.Json;
using System.Text.Json.Serialization;

namespace Banlab.Social.Api.Data
{
    public abstract class BaseEntity
    {
        [JsonPropertyName("id")]
        public string Id { get; protected set; }
    }

}
