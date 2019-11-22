using Microsoft.AspNetCore.Authorization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Calc.Web.Utils
{
    public class PermissionHandler : IAuthorizationHandler
    {
        public Task HandleAsync(AuthorizationHandlerContext context)
        {
            var pendingRequirements = context.PendingRequirements.ToList();

            foreach (var requirement in pendingRequirements)
            {
                if (requirement is AddPermissionRequirement)
                {
                    if (context.User.HasClaim(ClaimTypes.Name, "AddToken"))
                    {
                        context.Succeed(requirement);
                    }
                    else
                    {
                        context.Fail();
                    }
                }
                else if (requirement is MultiplicatePermissionRequirement)
                {
                    if (context.User.HasClaim(ClaimTypes.Name, "MultiplicateToken"))
                    {
                        context.Succeed(requirement);
                    }
                    else
                    {
                        context.Fail();
                    }
                }
            }

            return Task.CompletedTask;
        }
    }
}
