using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace FSTime.Infrastructure.Auth.Permissions;

public class PermissionAuthorizationPolicyProvider: DefaultAuthorizationPolicyProvider
{
    public PermissionAuthorizationPolicyProvider(IOptions<AuthorizationOptions> options) : base(options)
    {
    }

    public override async Task<AuthorizationPolicy?> GetPolicyAsync(string policyName)
    {
        var p = await base.GetPolicyAsync(policyName);
        if (p is not null) return p;

        return new AuthorizationPolicyBuilder()
            .AddRequirements(new PermissionRequirement(policyName))
            .Build();
    }
}