using FSTime.Application.Common.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Routing;

namespace FSTime.Infrastructure.Authorization.Permissions;

public class PolicyInspector(EndpointDataSource endpointDataSource) : IPolicyInspector
{
    public List<string> GetGroups()
    {
        var policies = new HashSet<string>();

        foreach (var e in endpointDataSource.Endpoints)
        {
            var authorizeAttributes = e.Metadata.OfType<AuthorizeAttribute>();
            foreach (var attribute in authorizeAttributes)
            {
                if (!string.IsNullOrEmpty(attribute.Policy))
                {
                    policies.Add(attribute.Policy);
                }
            }
        }

        var groups = new HashSet<string>();

        foreach (var p in policies)
        {
            if (p.Contains("TENANT"))
            {
                continue;
            }

            var items = p.Split('.');
            if (items.Length != 2)
            {
                continue;
            }

            groups.Add(items[0]);
        }

        return groups.ToList();
    }

}
