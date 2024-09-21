using ProtoBuf;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace Generators.Models
{
    [XmlRoot(Namespace = "http://my.schemas.org/Catalogs")]
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    public class Catalog
    {
        [JsonPropertyName("books")]
        public List<Book> Books { get; set; }
    }
}
