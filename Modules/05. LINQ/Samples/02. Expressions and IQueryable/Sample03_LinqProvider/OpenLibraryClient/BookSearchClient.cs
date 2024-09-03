using Newtonsoft.Json;
using Sample03.OpenLibraryClient.Models;
using System.Collections.Specialized;

namespace Sample03.OpenLibraryClient
{
    public record BookSearchRequest(
        string Query,
        string? Fields = null,
        string? Sort = null,
        string? Lang = null,
        int? Offset = null,
        int? Limit = null);

    public class BookSearchClient
    {
        private readonly Uri baseAddress = new("https://openlibrary.org");
        private static readonly UriTemplate.UriTemplate uriTemplate =
            new("/search.json?q={query}&fields={fields}&sort={sort}&lang={lang}&offset={offset}&limit={limit}");

        public BookSearchResponse Search(
            string query,
            string? fields = null,
            string? sort = null,
            string? lang = null,
            int? offset = null,
            int? limit = null)
        {
            var bindParams = new NameValueCollection();
            if (!string.IsNullOrEmpty(query))
                bindParams.Add("query", query);
            if (!string.IsNullOrEmpty(fields))
                bindParams.Add("fields", fields);
            if (!string.IsNullOrEmpty(sort))
                bindParams.Add("sort", sort);
            if (!string.IsNullOrEmpty(lang))
                bindParams.Add("lang", lang);
            if (offset.HasValue)
                bindParams.Add("offset", offset.ToString());
            if (limit.HasValue)
                bindParams.Add("limit", limit.ToString());

            var uri = uriTemplate.BindByName(baseAddress, bindParams);

            var client = new HttpClient();
            var resultString = client.GetStringAsync(uri).GetAwaiter().GetResult();

            return JsonConvert.DeserializeObject<BookSearchResponse>(resultString) ?? new BookSearchResponse();
        }

        public BookSearchResponse Search(BookSearchRequest request) =>
            Search(
                request.Query, request.Fields, request.Sort,
                request.Lang, request.Offset, request.Limit);
    }
}
