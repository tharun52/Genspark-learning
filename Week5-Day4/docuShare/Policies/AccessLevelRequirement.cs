using Microsoft.AspNetCore.Authorization;

namespace docuShare.Policies
{
    public class AccessLevelRequirement : IAuthorizationRequirement
    {
        public int RequiredAccessLevel { get; }
        public AccessLevelRequirement(int requiredAccessLevel)
        {
            RequiredAccessLevel = requiredAccessLevel;
        }
    }
}