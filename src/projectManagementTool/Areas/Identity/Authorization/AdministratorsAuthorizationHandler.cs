using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using projectManagementTool.Areas.Identity.Data;
using projectManagementTool.Models;

namespace projectManagementTool.Areas.Identity.Authorization
{
    public class AdministratorAuthorizationHandler : AuthorizationHandler<OperationAuthorizationRequirement, ProjectViewModel>
    {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            OperationAuthorizationRequirement requirement,
            ProjectViewModel resource
        )
        {
            if (context.User == null || resource == null)
            {
                return Task.CompletedTask;
            }

            if (
                requirement.Name != Constants.AddProjectOperationName
                && requirement.Name != Constants.UpdateProjectOperationName
                && requirement.Name != Constants.DeleteProjectOperationName
            )
            {
                return Task.CompletedTask;
            }

            if (context.User.IsInRole(Constants.AdministratorsRole))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}