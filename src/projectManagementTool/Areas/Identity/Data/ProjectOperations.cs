using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace projectManagementTool.Areas.Identity.Data
{
    public static class ProjectOperations
    {
        public static readonly OperationAuthorizationRequirement List =
            new OperationAuthorizationRequirement {Name = Constants.ListProjectsOperationName};
        
        public static readonly OperationAuthorizationRequirement Create =
            new OperationAuthorizationRequirement {Name = Constants.AddProjectOperationName};

        public static readonly OperationAuthorizationRequirement Update =
            new OperationAuthorizationRequirement {Name = Constants.UpdateProjectOperationName};

        public static readonly OperationAuthorizationRequirement Delete =
            new OperationAuthorizationRequirement {Name = Constants.DeleteProjectOperationName};
    }
}