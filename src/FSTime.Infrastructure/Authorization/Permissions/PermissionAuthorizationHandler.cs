using FSTime.Application.Common;
using FSTime.Application.Common.Interfaces;
using FSTime.Domain.AuthorizationAggregate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace FSTime.Infrastructure.Authorization.Permissions;

public class PermissionAuthorizationHandler(IServiceScopeFactory serviceScopeFactory)
    : AuthorizationHandler<PermissionRequirement>
{
    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context,
        PermissionRequirement requirement)
    {
        var userId = context.User.Claims.FirstOrDefault(x => x.Type == "sub")?.Value;

        if (!Guid.TryParse(userId, out var parsedUserId)) return;

        var tenantId = context.GetTenantIdFromAuthContext();
        if (tenantId is null) return;

        //Lets check if there are multiple requirements
        var requirements = new List<string>();
        if (requirement.Permission.Contains(","))
        {
            var items = requirement.Permission.Split(",");
            requirements.AddRange(items.Select(item => item.Trim()));
        }
        else
        {
            requirements.Add(requirement.Permission);
        }

        using var scope = serviceScopeFactory.CreateScope();

        foreach (var r in requirements)
        {
            var group = "";
            var action = "";
            if (r.Contains("."))
            {
                group = r.Split(".")[0];
                action = r.Split(".")[1];
            }
            else
            {
                group = r;
            }

            if (group == "TENANT")
            {
                var tenantRepository = scope.ServiceProvider.GetRequiredService<ITenantRepository>();
                if (tenantRepository is null) continue;

                if (context.Resource is null) continue;
                var ctx = (HttpContext)context.Resource;
                var tid = ctx.GetTenantIdFromHttpContext();

                if (tid is null) continue;
                //Für den Tenant, hat der User eine eigene Rolle in der TenantRole Tabelle.
                //In Action steht der Name der Rolle
                var hasRole = await tenantRepository.TenantUserHasRole(tid!.Value, parsedUserId, action);
                if (hasRole)
                {
                    context.Succeed(requirement);
                    return;
                }
            }

            if (action.Contains("_SELF"))
                switch (group)
                {
                    case "EMPLOYEE":
                        var employeeRepo = scope.ServiceProvider.GetRequiredService<IEmployeeRepository>();
                        if (employeeRepo is null) continue;

                        var emp = await employeeRepo.GetEmployeeByUserId(parsedUserId);
                        if (emp is null) continue;

                        if (context.Resource is null) continue;

                        var httpContext = (HttpContext)context.Resource;
                        var segments = httpContext?.Request?.Path.Value?.Split('/') ?? [];
                        if (!Guid.TryParse(segments[segments.Length - 1], out var parsedId)) continue;

                        if (emp.Id == parsedId)
                        {
                            context.Succeed(requirement);
                            return;
                        }

                        break;
                }

            var actionEnum = PermissionAction.Unknown;
            if (!string.IsNullOrEmpty(action))
                if (Enum.TryParse(action, true, out actionEnum) == false)
                    return;

            var permissionService = scope.ServiceProvider.GetRequiredService<IPermissionRepository>();
            var permissionResult = await permissionService.HasPermission(tenantId.Value, parsedUserId, group,
                actionEnum == PermissionAction.Unknown ? null : actionEnum);
            if (permissionResult) context.Succeed(requirement);
        }
    }
}