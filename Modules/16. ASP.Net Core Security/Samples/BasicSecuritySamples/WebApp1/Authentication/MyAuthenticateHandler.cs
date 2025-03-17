using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace WebApp1.Authentication
{
    public class MyAuthenticateHandler :
         AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private const string AuthenticationType = "MyAuth";
        private readonly string secPassw = "123";

        public MyAuthenticateHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder) : base(options, logger, encoder)
        {
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var name = Request.Query["name"].ToString();
            var passw = Request.Query["pass"].ToString();
            var roles = Request.Query["roles"].ToString();

            if (!string.IsNullOrEmpty(name) && passw == secPassw)
            {
                var principal = CreatePrincipal(name, roles);
                return AuthenticateResult.Success(
                    new AuthenticationTicket(principal, Scheme.Name));
            }

            return AuthenticateResult.NoResult();
        }

        private static ClaimsPrincipal CreatePrincipal(string userName, string roles = "")
        {
            var roleClaims = (roles.Split(",") ?? [])
                .Select(role => new Claim(ClaimTypes.Role, role));
            var claims = new List<Claim>
            {
                new(ClaimTypes.Name, userName),
                new(ClaimTypes.DateOfBirth, new DateTime(1982, 5, 1).ToShortDateString())
            };
            claims.AddRange(roleClaims);


            return new ClaimsPrincipal(
                new ClaimsIdentity(claims, AuthenticationType));
        }
    }
}
