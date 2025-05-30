using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

public class ModuleAccessRequirement : IAuthorizationRequirement
{
    public string ModuleName { get; }

    public ModuleAccessRequirement(string moduleName)
    {
        ModuleName = moduleName;
    }
}

public class ModuleAccessHandler : AuthorizationHandler<ModuleAccessRequirement>
{
    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        ModuleAccessRequirement requirement)
    {
        var userModuleClaims = context.User.FindAll("ModuleAccess");
        
        if (userModuleClaims.Any(c => c.Value == requirement.ModuleName))
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}
