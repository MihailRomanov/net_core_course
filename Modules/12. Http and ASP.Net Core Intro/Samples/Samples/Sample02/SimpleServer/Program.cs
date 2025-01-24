using Microsoft.Extensions.Primitives;
using Microsoft.Net.Http.Headers;
using System.Net.Mime;

namespace SimpleServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var app = builder.Build();

            app.Run(ProcessRequest);

            app.Run();
        }

        private class RequestData
        {
            public string Method { get; internal set; }
            public StringValues DateRaw { get; internal set; }
            public DateTimeOffset? DateTyped { get; internal set; }
            public Guid Id { get; internal set; }
            public PathString Path { get; internal set; }
            public string QueryStringParams { get; internal set; }
            public string BodyString { get; internal set; }
        }

        private static async Task ProcessRequest(HttpContext context)
        {
            var request = context.Request;
            var response = context.Response;

            var requestData = new RequestData();

            // Метод
            requestData.Method = request.Method;

            // Заголовки (просто строкой и типизированно)
            requestData.DateRaw = request.Headers.Date;
            requestData.DateTyped = context.Request.GetTypedHeaders().Date;

            // Нестандартный заголовок
            if (!Guid.TryParse(request.Headers["X-Trace-Id"], out var id))
                id = Guid.Empty;

            requestData.Id = id;

            // Строка запроса
            requestData.Path = request.Path;
            requestData.QueryStringParams =
                string.Join("\n", request.Query.Select(t => $"[{t.Key}] = {t.Value}"));

            // Тело запроса
            using var reader = new StreamReader(request.Body);
            requestData.BodyString = await reader.ReadToEndAsync();

            var acceptPlainText = request.GetTypedHeaders().Accept
                .Contains(new MediaTypeHeaderValue(MediaTypeNames.Text.Plain));

            response.StatusCode = StatusCodes.Status200OK;
            var (responseString, contentType) = acceptPlainText
                ? TextResponse(requestData)
                : HtmlResponse(requestData);
            response.GetTypedHeaders().ContentType = new MediaTypeHeaderValue(contentType);
            await response.WriteAsync(responseString);
        }

        private static (string content, string contentType) TextResponse(RequestData requestData)
        {
            var content = $"""
                Получен запрос {requestData.Method}
                На дату {requestData.DateRaw} / {requestData.DateTyped} c id = {requestData.Id}
                По пути {requestData.Path} c параметрами: 
                {requestData.QueryStringParams} 
                И телом
                {requestData.BodyString}
                """;
            return (content, MediaTypeNames.Text.Plain);
        }

        private static (string content, string contentType) HtmlResponse(RequestData requestData)
        {
            var content = $"""
                <html>
                <head>
                    <meta charset="UTF-8">
                    <title>Получен запрос</title>
                </head>
                <body>
                <h1>Получен запрос {requestData.Method}</h1>
                На дату {requestData.DateRaw} / {requestData.DateTyped} c id = {requestData.Id} <br/>
                По пути {requestData.Path} c параметрами: <br/>
                {requestData.QueryStringParams} <br/>
                И телом <br/>
                {requestData.BodyString} <br/>
                </body>
                </html>
                """;
            return (content, MediaTypeNames.Text.Html);

        }
    }
}
