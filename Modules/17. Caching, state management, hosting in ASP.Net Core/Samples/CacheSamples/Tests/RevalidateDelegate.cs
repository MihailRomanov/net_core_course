using System.Collections.Concurrent;
using System.Net;

namespace Tests
{
    public class RevalidateDelegate : DelegatingHandler
    {
        ConcurrentDictionary<Uri, (DateTimeOffset, byte[])> cachedResponses =
            new ConcurrentDictionary<Uri, (DateTimeOffset, byte[])>();

        protected override async Task<HttpResponseMessage> SendAsync(
                HttpRequestMessage request,
                CancellationToken cancellationToken)
        {
            (DateTimeOffset, byte[]) cached;

            if (cachedResponses.TryGetValue(request.RequestUri!,
                out cached))
            {
                request.Headers.IfModifiedSince = cached.Item1;
            }

            var result = await base.SendAsync(request, cancellationToken);

            if (result.StatusCode == HttpStatusCode.NotModified)
            {
                return new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new ByteArrayContent(cached.Item2)
                };
            }

            var lastModified = result.Content.Headers.LastModified;
            if (lastModified.HasValue)
            {
                var content = await result.Content.ReadAsByteArrayAsync();
                cachedResponses.AddOrUpdate(request.RequestUri,
                    (_) => (lastModified.Value, content),
                    (_, _) => (lastModified.Value, content));
            }

            return result;
        }
    }

}
