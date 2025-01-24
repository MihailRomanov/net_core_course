namespace AuthLib
{
    public class ExtendedAuthMiddleware
    {
        private readonly RequestDelegate next;
        private readonly string passwd;
        private readonly int MaxFailCount;

        public ExtendedAuthMiddleware(RequestDelegate next, string passwd, int maxFailCount = 5)
        {
            this.next = next;
            this.passwd = passwd;
            MaxFailCount = maxFailCount;
        }

        public async Task Invoke(HttpContext context, IFailCountStore failCountStore)
        {
            if ((context.Request.Path == "/favicon.ico") ||
                (context.Request.Query["pass"] == passwd && failCountStore.Fails < MaxFailCount))
            {
                await next(context);
            }
            else
            {
                failCountStore.AddFail();
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync($"Unauthorized. Tries: {failCountStore.Fails}");
            }
        }
    }
}
