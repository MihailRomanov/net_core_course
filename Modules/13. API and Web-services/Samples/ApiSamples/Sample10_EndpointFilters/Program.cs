namespace Sample10EndpointFilters
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllers();
            var app = builder.Build();

            app.MapGet("/", (string p1="", string p2="") => $"{p1} - {p2}")
                .AddEndpointFilter(async (invocationContext, next) => 
                {
                    foreach (var arg in invocationContext.Arguments)
                    {
                        if (arg is string s && string.IsNullOrEmpty(s))
                            return Results.BadRequest();
                    }
                    return await next.Invoke(invocationContext);
                });

            app.MapGet("/t", (string p1 = "", string p2 = "") => $"{p1} - {p2}")
                .AddEndpointFilter<NoEmptyFilter>();

            app.MapControllers()
                .AddEndpointFilter<IEndpointConventionBuilder, NoEmptyFilter>();

            app.Run();
        }
    }
}
