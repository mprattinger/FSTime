namespace FSTime.Domain.Common.ValueObjects;

public class TenantRole : ValueObject
{
    public Guid UserId { get; }
    public Guid TenantId { get; }
    public string RoleName { get; } = null!;

    public TenantRole(Guid tenantId, Guid userId, string roleName)
    {
        UserId = userId;
        TenantId = tenantId;
        RoleName = roleName;
    }

    private TenantRole() { }

    public override IEnumerable<object?> GetEqualityComponents()
    {
        yield return UserId;
        yield return TenantId;
        yield return RoleName;
    }
}