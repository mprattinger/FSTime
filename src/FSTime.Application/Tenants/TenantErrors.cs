using ErrorOr;
using FSTime.Domain.TenantAggregate;

namespace FSTime.Application.Tenants;

public static class TenantErrors
{
    public static Error Creation_Error(string tenantName, string error) => Error.Conflict("TENANT_HANDLER.GEN_ERROR",
        $"When creating the tenant {tenantName} an error occured: {error}");
    
    public static Error Tenant_Lookup_Error(string error) => Error.Conflict("TENANT_HANDLER.GEN_ERROR",
        $"When trying to get the tenant an error occured: {error}");
    
    public static Error Tenant_AssignUser_Error(string error) => Error.Conflict("TENANT_ASSIGN_USER.GEN_ERROR",
        $"When trying to add an user to the tenant an error occured: {error}");

    public static Error Tenant_Lookup_UserId_NotFound() => Error.NotFound("TENANT_HANDLER.WITH_USERID_NOT_FOUND",
        $"When trying to get the tenant by user id, the tenant was not found");
    
    public static Error Tenant_ById_NotFound() => Error.NotFound("TENANT_HANDLER.WITH_TENANT_ID_NOT_FOUND",
        $"When trying to get the tenant by id, the tenant was not found");
    
    public static Error  Tenant_User_Already_Assigned(Guid tenantId, Guid userId) => Error.Conflict("TENANT_ASSIGN_USER.USER_ALREADY_ASSIGNED",
        $"The user with id {userId} is already assigned to the tenant with id {tenantId}");
}