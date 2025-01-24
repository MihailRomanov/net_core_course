using Microsoft.Extensions.FileProviders;

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

        private static async Task ProcessRequest(HttpContext context)
        {
            var fileProvider = new EmbeddedFileProvider(typeof(Program).Assembly);
            var request = context.Request;
            switch (request.Path.Value)
            {
                case "/form" when HttpMethods.IsGet(request.Method):
                    await context.Response.SendFileAsync(fileProvider.GetFileInfo("/Forms.form.html"));
                    return;
                case "/form" when HttpMethods.IsPost(request.Method):
                    await context.ProcessLogin();
                    return;

                case "/form_with_file" when HttpMethods.IsGet(request.Method):
                    await context.Response.SendFileAsync(fileProvider.GetFileInfo("/Forms.form_with_file.html"));
                    return;
                case "/form_with_file" when HttpMethods.IsPost(request.Method):
                    await context.ProcessFileUpload();
                    return;

                case "/cookie_counter":
                    await context.CookieCounter();
                    return;

                case "/object":
                    await context.ProcessObject();
                    return;

                case "/generate" when HttpMethods.IsGet(request.Method):
                    await context.Response.SendFileAsync(fileProvider.GetFileInfo("/Forms.generate_doc.html"));
                    return;
                case "/generate" when HttpMethods.IsPost(request.Method):
                    await context.GenerateDocument();
                    return;

            }
        }
    }
}
