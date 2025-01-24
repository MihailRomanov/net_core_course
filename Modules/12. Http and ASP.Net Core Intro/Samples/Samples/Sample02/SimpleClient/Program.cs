using System.Net.Http.Headers;
using System.Net.Mime;

namespace SimpleClient
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            await SimpleRequests();
            await ExtendedRequest();            
        }

        static async Task SimpleRequests()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:5000");

            var simpleGetResult = await client.GetStringAsync("/");
            Console.WriteLine(simpleGetResult);
            Console.WriteLine("======");

            await client.PostAsync(
                    "/p1/p2", new StringContent("Simple string"));

            var simplePostResult =
                await (await client.PostAsync(
                    "/p1/p2", new StringContent("Simple string")))
                .Content.ReadAsStringAsync();
            Console.WriteLine(simplePostResult);
            Console.WriteLine("======");
        }

        static async Task ExtendedRequest()
        {
            var client = new HttpClient(
                new SocketsHttpHandler { ConnectTimeout = TimeSpan.FromSeconds(10) });
            client.BaseAddress = new Uri("http://localhost:5000");

            var message = new HttpRequestMessage
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri("/p1/p2?A1=12&A2=454545", UriKind.Relative),
                Content = new StringContent("String content")
            };
            message.Headers.Date = DateTimeOffset.Now;
            message.Headers.Add("X-Trace-Id", Guid.NewGuid().ToString());
            message.Headers.Accept.Add(
                new MediaTypeWithQualityHeaderValue(MediaTypeNames.Text.Plain));

            var result = await (await client.SendAsync(message))
                .Content.ReadAsStringAsync();
            Console.WriteLine(result);
        }
    }
}
