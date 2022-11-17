using Microsoft.AspNetCore.Authorization;
using Saitynai.Auth.Model;
using System.Threading.Tasks;

namespace Saitynai.Auth
{
    public class SameUserAuthorizationHandler : AuthorizationHandler<SameUserRequirement, IUserOwnedResource>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, SameUserRequirement requirement,
            IUserOwnedResource resource)
        {
            if (context.User.IsInRole(UserRoles.Admin) || 
                context.User.FindFirst(CustomClaims.UserId).Value == resource.UserId)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }

    public record SameUserRequirement : IAuthorizationRequirement;
}
