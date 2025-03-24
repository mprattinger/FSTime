namespace FSTime.Contracts.Common.Exceptions.Tenants;

public class TenantNotFoundException : Exception
{
    public TenantNotFoundException(Guid tenantId):base($"Couldn't find tenant {tenantId}")
    {
    }
}