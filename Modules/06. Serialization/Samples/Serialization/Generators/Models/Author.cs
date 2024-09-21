using ProtoBuf;
using System.Text.Json.Serialization;

namespace Generators.Models
{
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    public class Author
    {
        [JsonPropertyName("name")]
        public string FirstName { get; set; }
        [JsonPropertyName("last_name")]
        public string LastName { get; set; }
        [JsonPropertyName("year")]
        public int BirthYear { get; set; }
    }
}