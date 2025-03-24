namespace FSTime.Contracts.Common.Exceptions.Tenants;

public class UserAlreadyAsignedException : Exception
{
    public UserAlreadyAsignedException(Guid tenantId, Guid userId):base($"User {userId} already assigned to tenant {tenantId}")
    {
        
    }
}