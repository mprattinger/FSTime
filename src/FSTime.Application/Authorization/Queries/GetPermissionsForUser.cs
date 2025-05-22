using ErrorOr;
using FlintSoft.CQRS.Handlers;
using FlintSoft.CQRS.Interfaces;
using FSTime.Application.Common.Interfaces;
using FSTime.Domain.AuthorizationAggregate;

namespace FSTime.Application.Authorization.Queries;

public static class GetPermissionsForUser
{
    public record Query(Guid TenantId, Guid UserId) : IQuery<List<Permission>>;

    internal sealed class Handler(IPermissionRepository permissionRepository, ITenantRepository tenantRepository)
        : IQueryHandler<Query, List<Permission>>
    {
        public async Task<ErrorOr<List<Permission>>> Handle(Query request, CancellationToken cancellationToken)
        {
            var ret = new List<Permission>();

            var permissions = await permissionRepository.GetPermissions(request.TenantId, request.UserId);
            if (permissions.Any())
            {
                ret.AddRange(permissions);
            }

            //Prüfen auf TENANT Role
            var tenants = await tenantRepository.GetTenantRoles(request.TenantId, request.UserId);
            foreach (var t in tenants)
            {
                var u = t.Users.First(x => x.UserId == request.UserId);
                if (u is not null)
                {
                    var p = new Permission(t.Id, u.UserId, group: $"TEMANT.{u.RoleName}", PermissionAction.All);
                    ret.Add(p);
                }
            }

            if (!ret.Any())
            {
                return AuthorizationErrors.NoPermissions();
            }

            return ret;
        }
    }
}