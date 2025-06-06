using Microsoft.AspNetCore.Authorization;

namespace docuShare.Policies
{
    public class AccessLevelHandler : AuthorizationHandler<AccessLevelRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AccessLevelRequirement requirement)
        {
            var accessLevelClaim = context.User.FindFirst("AccessLevel");
            if (accessLevelClaim != null && int.TryParse(accessLevelClaim.Value, out int userAccessLevel))
            {
                if (userAccessLevel >= requirement.RequiredAccessLevel)
                {
                    context.Succeed(requirement);
                }
            }
            return Task.CompletedTask;
        }
    }
}