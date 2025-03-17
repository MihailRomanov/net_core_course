using System.Security.Claims;
using System.Security.Principal;

namespace IdentityApiSamples
{
    public class ClaimsAPI
    {
        [SetUp]
        public void SetUp()
        {
            ClaimsPrincipal.ClaimsPrincipalSelector = null;
        }

        [Test]
        public void PrintAllClaims()
        {
            AppDomain.CurrentDomain
                .SetPrincipalPolicy(PrincipalPolicy.WindowsPrincipal);

            var principal = ClaimsPrincipal.Current;

            principal.Claims.ToList().ForEach(cl => Console.WriteLine($"{cl.Type} : {cl.Value}"));
        }

        [Test]
        public void GetNeededClaims()
        {
            AppDomain.CurrentDomain
                .SetPrincipalPolicy(PrincipalPolicy.WindowsPrincipal);

            var principal = ClaimsPrincipal.Current;

            var nameClaim = principal.FindFirst(ClaimTypes.Name);
            var dennySidClaims = principal.FindAll(ClaimTypes.DenyOnlySid);

            Console.WriteLine(nameClaim.Value);

            dennySidClaims.ToList().ForEach(cl => Console.WriteLine(cl.Value));
        }

        [Test]
        public void CreateIdentity()
        {
            var claims = new Claim[]
                {
                    new Claim(ClaimTypes.Name, "Mihail Romanov"),
                    new Claim(ClaimTypes.Role, "Manager"),
                    new Claim(ClaimTypes.Role, "Power User"),
                    new Claim("http://epam.com/claims/upsaId", "4060741400005280615")
                };

            var newPrincipal = new ClaimsPrincipal(new ClaimsIdentity(claims));

            ClaimsPrincipal.ClaimsPrincipalSelector = () => newPrincipal;

            var principal = ClaimsPrincipal.Current;
            Console.WriteLine(principal.Identities.First().Name);
        }


        [Test]
        public void CheckRole()
        {
            var claims = new Claim[]
                {
                    new Claim(ClaimTypes.Name, "Mihail Romanov"),
                    new Claim(ClaimTypes.Role, "Manager"),
                    new Claim(ClaimTypes.Role, "Power User"),
                    new Claim("http://epam.com/claims/upsaId", "4060741400005280615")
                };

            var principal = new ClaimsPrincipal(new ClaimsIdentity(claims));

            Assert.That(principal.IsInRole("Manager"), Is.True);
            Assert.That(principal.IsInRole("Manager1"), Is.False);
        }
    }
}
