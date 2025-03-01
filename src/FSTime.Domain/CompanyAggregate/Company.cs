using FSTime.Domain.Common;
using FSTime.Domain.TenantAggregate;

namespace FSTime.Domain.CompanyAggregate;

public class Company: AggregateRoot
{
    public string Name { get; set; } = "";

    public Guid TenantId { get; set; }
    
    public Tenant? Tenant { get; set; }
    
    public Company(string name, Guid tenantId, Guid? id = null) : base(id ?? Guid.CreateVersion7())
    {
        Name = name;
        TenantId = tenantId;
    }

    private Company()
    {
    }
}