using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace WebApp1.Authorization
{
    public class LegalAgeRequirement(int legalAge) : IAuthorizationRequirement
    {
        public int LegalAge { get; } = legalAge;
    }

    public class LegalAgeHandler : AuthorizationHandler<LegalAgeRequirement>
    {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context, LegalAgeRequirement requirement)
        {
            var dateOfBirthClaim = context.User.FindFirst(ClaimTypes.DateOfBirth);

            if (dateOfBirthClaim != null
                && DateTime.TryParse(dateOfBirthClaim.Value, out var dateOfBirth)
                && dateOfBirth.AddYears(requirement.LegalAge) <= DateTime.Now)
            {
                context.Succeed(requirement);
            }
            else
            {
                context.Fail();
            }
            
            return Task.CompletedTask;
        }
    }
}
