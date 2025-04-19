using FSTime.Domain.Common;
using FSTime.Domain.TenantAggregate;
using FSTime.Domain.UserAggregate;

namespace FSTime.Domain.AuthorizationAggregate;

public enum PermissionAction
{
    Unknown = 0,
    Read = 1,
    Update = 2,
    Delete = 3,
    All = 4
}

public class Permission : AggregateRoot
{
    public Permission(Guid tenantId, Guid userId, string group, PermissionAction action) : this(tenantId, userId, group)
    {
        Action = action;
    }

    public Permission(Guid tenantId, User user, string group, PermissionAction action) : this(tenantId, user, group)
    {
        Action = action;
    }

    public Permission(Guid tenantId, Guid userId, string group) : this(tenantId, userId)
    {
        Group = group;
        Action = PermissionAction.All;
    }

    public Permission(Guid tenantId, User user, string group) : this(tenantId, user)
    {
        Group = group;
        Action = PermissionAction.All;
    }

    public Permission(Guid tenantId, Guid userId)
    {
        TenantId = tenantId;
        UserId = userId;
    }

    public Permission(Guid tenantId, User user)
    {
        UserId = user.Id;
        User = user;
    }

    private Permission()
    {
    }

    public Guid TenantId { get; private set; }
    public Tenant? Tenant { get; private set; }

    public Guid UserId { get; private set; }
    public User? User { get; private set; }

    public string Group { get; private set; } = null!;
    public PermissionAction Action { get; private set; }

    public void SetGroup(string group)
    {
        Group = group;
    }

    public void SetAction(PermissionAction action)
    {
        Action = action;
    }
}