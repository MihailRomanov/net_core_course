using AspNet.Security.OAuth.Yandex;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;

namespace WebApp2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddRazorPages();
            builder.Services.AddHttpContextAccessor();

            builder.Services.AddAuthorization(options =>
            {
                //options.FallbackPolicy = new AuthorizationPolicyBuilder()
                //    .RequireAuthenticatedUser()
                //    .Build();
            });

            builder.Services.AddAuthentication(
                    CookieAuthenticationDefaults.AuthenticationScheme)
                .AddYandex(op =>
                {
                    op.ClientId = "";
                    op.ClientSecret = "";
                    op.CallbackPath = "/signin-yandex";
                })
                .AddCookie();

            var app = builder.Build();

            app.UseAuthentication();
            app.UseAuthorization();
            app.MapRazorPages();

            app.Map("/account/authinyandex", [AllowAnonymous] (HttpContext context) =>
            {
                var isAuthenticate = context.User.Identity?.IsAuthenticated ?? false;
                if (!isAuthenticate)
                    context.ChallengeAsync(YandexAuthenticationDefaults.AuthenticationScheme);
                else
                    context.Response.Redirect("/");
            }).WithName("authinyandex");

            app.Run();
        }
    }
}
