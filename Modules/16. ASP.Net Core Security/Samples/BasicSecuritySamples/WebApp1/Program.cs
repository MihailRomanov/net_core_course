using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using WebApp1.Authentication;
using WebApp1.Authorization;

namespace WebApp1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddRazorPages();
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddAuthentication()
                .AddScheme<AuthenticationSchemeOptions, MyAuthenticateHandler>("MyScheme", o => { });
            builder.Services.AddSingleton<IAuthorizationHandler, LegalAgeHandler>();

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("LegalAge", builder => builder.RequireAssertion((context) =>
                {
                    var dateOfBirthClaim = context.User.FindFirst(ClaimTypes.DateOfBirth);
                    return dateOfBirthClaim != null
                        && DateTime.TryParse(dateOfBirthClaim.Value, out var dateOfBirth)
                        && dateOfBirth.AddYears(18) <= DateTime.Now;
                }));

                options.AddPolicy("ManagerAndLegalAge", builder =>
                    builder
                        .AddRequirements(new LegalAgeRequirement(18))
                        .RequireRole("Manager")
                    );

                options.FallbackPolicy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();

            });

            var app = builder.Build();

            app.UseAuthentication();
            app.UseAuthorization();
            
            app.MapRazorPages();

            app.Run();
        }
    }
}
