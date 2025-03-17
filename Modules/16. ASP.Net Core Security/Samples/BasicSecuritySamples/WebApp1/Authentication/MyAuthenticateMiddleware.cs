
using System.Security.Claims;

namespace WebApp1.Authentication
{
    internal class MyAuthenticateMiddleware(RequestDelegate next)
    {
        private const string AuthenticationType = "MyAuth";
        private readonly string secPassw = "123";

        public async Task InvokeAsync(HttpContext context)
        {
            var name = context.Request.Query["name"].ToString();
            var passw = context.Request.Query["pass"].ToString();

            if (!string.IsNullOrEmpty(name) && passw == secPassw)
            {
                context.User = CreatePrincipal(name);
            }
            await next(context);
        }

        private static ClaimsPrincipal CreatePrincipal(string userName)
        {
            var claims = new Claim[]
            {
                new(ClaimTypes.Name, userName),
                new(ClaimTypes.Role, "Manager"),
            };

            return new ClaimsPrincipal(
                new ClaimsIdentity(claims, AuthenticationType));
        }
    }
}