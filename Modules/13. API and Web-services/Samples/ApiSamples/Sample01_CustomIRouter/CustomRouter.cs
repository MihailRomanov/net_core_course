namespace Sample01_CustomIRouter
{
    internal class CustomRouter : IRouter
    {
        private readonly Dictionary<string, RequestDelegate> handlers =
            new(StringComparer.OrdinalIgnoreCase)
            {
                ["/api/one"] = 
                    async (context) => await context.Response.WriteAsync("1"),
                ["/api/two"] =
                    async (context) => await context.Response.WriteAsync("2"),
                ["/api/three"] =
                    async (context) => await context.Response.WriteAsync("3"),
                ["/"] =
                    async (context) => await context.Response.WriteAsync("3"),
            };

        public VirtualPathData? GetVirtualPath(VirtualPathContext context) 
            => throw new NotImplementedException();

        public async Task RouteAsync(RouteContext context)
        {
            var headers = context.HttpContext.Request.Headers;
            if (headers.TryGetValue("XXX", out var key) &&
                handlers.TryGetValue(key!, out var handler))
            {
                context.Handler = handler;
            }
        }
    }
}