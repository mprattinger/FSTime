using FSTime.Application.Common.Interfaces;
using FSTime.Contracts.Authorization;
using FSTime.Infrastructure.Common.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace FSTime.Infrastructure.Authorization.Permissions;

public class PermissionAuthorizationHandler(IServiceScopeFactory serviceScopeFactory)
: AuthorizationHandler<PermissionRequirement>
{
    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
    {
        string? userId = context.User.Claims.FirstOrDefault(x => x.Type == "sub")?.Value;

        if (!Guid.TryParse(userId, out var parsedUserId))
        {
            return;
        }

        var group = "";
        var action = "";
        if (requirement.Permission.Contains("."))
        {
            group = requirement.Permission.Split(".")[0];
            action = requirement.Permission.Split(".")[1];
        } else {
            group = requirement.Permission;
        }
        
        using var scope = serviceScopeFactory.CreateScope();
        
        if (group == "TENANT")
        {
            var tenantRepository = scope.ServiceProvider.GetRequiredService<ITenantRepository>();
            if (tenantRepository is null)
            {
                return;
            }
            
            if (context.Resource is null)
            {
                return;
            }
            var ctx = (HttpContext)context.Resource;
            var tid = ctx.GetTenantIdFromHttpContext();

            if (tid is null)
            {
                return;
            }
            //Für den Tenant, hat der User eine eigene Rolle in der TenantRole Tabelle.
            //In Action steht der Name der Rolle
            var hasRole = await tenantRepository.GetUsersTenantRole(tid!.Value, parsedUserId, action);
            if (hasRole)
            {
                context.Succeed(requirement);
            }

            return;
        }

        if (action.Contains(("_SELF")))
        {
            switch (group)
            {
                case "EMPLOYEE":
                    var employeeRepo = scope.ServiceProvider.GetRequiredService<IEmployeeRepository>();
                    if (employeeRepo is null)
                    {
                        return;
                    }
                    
                    break;
            }
        }
        
        var permissionService = scope.ServiceProvider.GetRequiredService<IPermissionService>();
        var permissionResult = await permissionService.HasPermission(parsedUserId, group, action);
        if (permissionResult)
        {
            context.Succeed(requirement);
        }
        
        return;
    }
}