using Newtonsoft.Json;

namespace Sample03.OpenLibraryClient.Models
{
    public class BookSearchResponse
    {
        [JsonProperty("start")]
        public int Start { get; set; }

        [JsonProperty("num_found")]
        public int Found { get; set; }
        
        [JsonProperty("docs")]
        public List<Book> Books { get; set; } = new List<Book>();
    }
}
