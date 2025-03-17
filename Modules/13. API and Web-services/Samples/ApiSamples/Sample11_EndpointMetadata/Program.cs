namespace Sample11_EndpointMetadata
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllers();
            var app = builder.Build();

            var group = app.MapGroup("");
            group
                .MapGet("/", (string? p1, string? p2) => $"{p1} - {p2}")
                .WithMetadata(new ValidationOptionMetadata(ValidationOption.NotNull));
            group.MapControllers();
            // .WithMetadata(new ValidationOptionMetadata(ValidationOption.NotNull));
            group.AddEndpointFilter<NoEmptyFilter>();

            app.Run();
        }
    }
}
