using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.Extensions.Primitives;
using Microsoft.Net.Http.Headers;
using MigraDoc.DocumentObjectModel;
using MigraDoc.Rendering;
using OfficeIMO.Word;
using PdfSharp.Pdf;
using System.Net.Mime;
using System.Text;

namespace SimpleServer
{
    public static class ProcessHelper
    {
        public static async Task CookieCounter(this HttpContext context)
        {
            var request = context.Request;

            var counter = Convert.ToInt32(
                request.Cookies["counter"] ?? "0");

            counter++;
            context.Response.Cookies.Append("counter", counter.ToString());
            await context.Response.WriteAsync($"Current counter {counter}\n");
        }

        public static async Task ProcessFileUpload(this HttpContext context)
        {
            var request = context.Request;

            if (request.HasFormContentType)
            {
                var docId = request.Form["doc_id"];
                var docName = request.Form["doc_name"];

                await context.Response.WriteAsync($"Uploaded {docName}({docId}) with files:\n");

                foreach (var file in request.Form.Files)
                {
                    await context.Response.WriteAsync($"{file.Name} {file.FileName} {file.ContentType} {file.Length}\n");
                    await context.Response.WriteAsync(new StreamReader(file.OpenReadStream()).ReadToEnd());
                    await context.Response.WriteAsync("\n");
                }
            }
            else
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
            }
        }

        public static async Task ProcessLogin(this HttpContext context)
        {
            var request = context.Request;

            if (request.HasFormContentType)
            {
                var login = request.Form["login"].ToString();
                var password = request.Form["password"].ToString();
                var rememberMe = request.Form["remember_me"];

                if (!(login.Length > 3 && password.StartsWith("123")))
                {
                    context.Response.StatusCode = StatusCodes.Status403Forbidden;
                    await context.Response.WriteAsync($"User {login} with password {password} not correct");
                }
                else
                {
                    if (rememberMe == "true")
                    {
                        await SetAuthCookie(context, login, password);
                    }
                    await context.Response.WriteAsync($"User {login} with password {password} are login");
                }
            }
            else
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
            }
        }

        private static async Task SetAuthCookie(HttpContext context, string login, string password)
        {
            var request = context.Request;

            var options = new CookieOptions()
            {
                Expires = DateTimeOffset.Now.AddDays(15),
                Secure = false,
            };

            context.Response.Cookies.Append("auth", $"{login}|||{password}", options);
        }


        public static async Task ProcessObject(this HttpContext context)
        {
            var request = context.Request;

            if (request.HasJsonContentType())
            {
                var dto = await request.ReadFromJsonAsync<TestDto>();

                var newDto = dto with
                {
                    A = dto.A * 4,
                    B = dto.B + dto.A,
                };

                await context.Response.WriteAsJsonAsync(newDto);
            }
        }

        public static async Task GenerateDocument(this HttpContext context)
        {
            var request = context.Request;

            if (request.HasFormContentType)
            {
                var name = request.Form["name"].ToString();
                var type = request.Form["type"].ToString();
                var content = request.Form["content"].ToString();

                byte[] document = GenerateDocument(type, content);

                var response = context.Response;
                response.GetTypedHeaders().ContentDisposition =
                    new ContentDispositionHeaderValue("attachment")
                    {
                        FileName = name + "." + type,
                    };
                response.GetTypedHeaders().ContentType
                    = new MediaTypeHeaderValue(GetContentType(type));

                await response.Body.WriteAsync(document);

            }
            else
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
            }
        }

        private static string GetContentType(string type)
        {
            return type switch
            {
                "txt" => MediaTypeNames.Text.Plain,
                "docx" => "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
                "pdf" => MediaTypeNames.Application.Pdf,
                _ => throw new ArgumentOutOfRangeException(nameof(type))
            };
        }

        private static byte[] GenerateDocument(string type, string content)
        {
            switch (type)
            {
                case "txt":
                    return Encoding.UTF8.GetBytes(content);
                case "docx":
                    {
                        using var document = WordDocument.Create();
                        using var stream = new MemoryStream();

                        var paragraph = document.AddParagraph(content);
                        paragraph.Color = SixLabors.ImageSharp.Color.Blue;

                        document.Save(stream);
                        return stream.ToArray();
                    }
                case "pdf":
                    {
                        using var stream = new MemoryStream();
                        var document = new MigraDoc.DocumentObjectModel.Document();
                        document
                            .AddSection()
                            .AddParagraph()
                            .AddFormattedText(content, TextFormat.Bold);
                        var renderer = new PdfDocumentRenderer()
                        {
                            Document = document,
                        };
                        renderer.RenderDocument();
                        renderer.PdfDocument.Save(stream);
                        return stream.ToArray();
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type));
            }
        }
    }
}
