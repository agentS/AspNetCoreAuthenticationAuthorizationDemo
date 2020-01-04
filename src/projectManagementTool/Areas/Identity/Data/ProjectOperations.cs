using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace projectManagementTool.Areas.Identity.Data
{
    public static class ProjectOperations
    {
        public static OperationAuthorizationRequirement Create =
            new OperationAuthorizationRequirement {Name = Constants.AddProjectOperationName};

        public static OperationAuthorizationRequirement Update =
            new OperationAuthorizationRequirement {Name = Constants.UpdateProjectOperationName};

        public static OperationAuthorizationRequirement Delete =
            new OperationAuthorizationRequirement {Name = Constants.DeleteProjectOperationName};
    }
}