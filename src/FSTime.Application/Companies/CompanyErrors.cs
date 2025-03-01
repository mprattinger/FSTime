using ErrorOr;

namespace FSTime.Application.Companies;

public static class CompanyErrors
{
    public static Error Create_Company_Error(string error) => Error.Conflict("COMPANY_HANDLER.CREATE.GEN_ERROR",
        $"When trying to create a company, the following error occured: {error}");
    
    public static Error Create_Company_Tenant_NotLicences() => Error.Unauthorized("COMPANY_HANDLER.CREATE.TENANT_NOT_LICENCED",
        "The tenant is not licenced, so a company can not be created");
    
    public static Error Create_Company_Exists(string name) => Error.Unauthorized("COMPANY_HANDLER.CREATE.COMPANY_EXISTS",
        $"Company with name {name} already exists in this tenant");

    public static Error Create_Company_Tenant_NotFound() => Error.NotFound("COMPANY_HANDLER.CREATE.TENANT_NOT_FOUND",
        "The tenant could not be found, so a company can not be created");
    
    public static Error Get_Company_By_Tenant_Error(string error) => Error.Conflict("COMPANY_HANDLER.QUERY.BY_TENANT.GEN_ERROR",
        $"Error retrieving company by tenant: {error}");
}