using Newtonsoft.Json;

namespace Sample03.OpenLibraryClient.Models
{
    public class Book
    {
        [JsonProperty("key")]
        public string Key { get; set; } = "";

        [JsonProperty("title")]
        public string Title { get; set; } = "";

        [JsonProperty("author_name")]
        public string[] Author { get; set; } = [];

        [JsonProperty("first_publish_year")]
        public int FirstPublishYear { get; set; }

        [JsonProperty("publish_year")]
        public int[] PublishYear { get; set; } = [];

        [JsonProperty("number_of_pages_median")]
        public int NumberOfPagesMedian { get; set; }

        [JsonProperty("isbn")]
        public string[] Isbn { get; set; } = [];

        [JsonProperty("publisher")]
        public string[] Publisher { get; set; } = [];

        [JsonProperty("language")]
        public string[] Language { get; set; } = [];

        [JsonProperty("has_fulltext")]
        public bool HasFulltext { get; set; }
    }
}