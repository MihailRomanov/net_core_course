using ProtoBuf;
using System.Text.Json.Serialization;

namespace Generators.Models
{
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    public class Book
    {
        [JsonPropertyName("title")]
        public string Title { get; set; }
        [JsonPropertyName("autor_names")]
        public List<Author> Authors { get; set; }
        [JsonPropertyName("page_count")]
        public int Pages { get; set; }
    }
}
