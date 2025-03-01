using Microsoft.AspNetCore.Authorization;

namespace FSTime.Infrastructure.Auth.Permissions;

public class PermissionRequirement
: IAuthorizationRequirement
{
    public string Permission { get; }

    public PermissionRequirement(string permission)
    {
        Permission = permission;
    }
}