namespace FSTime.Contracts.Common.Exceptions.Tenants;

public class UserNotAssignedException : Exception
{
    public UserNotAssignedException(Guid tenantId, Guid userId):base($"User {userId} not assigned to tenant {tenantId}")
    {
        
    }
}