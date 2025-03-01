using FSTime.Domain.Common;
using FSTime.Domain.Common.ValueObjects;
using FSTime.Domain.CompanyAggregate;

namespace FSTime.Domain.TenantAggregate;

public class Tenant : AggregateRoot
{
    public string Name { get; set; } = "";

    public bool IsLicensed { get; set; }

    public List<TenantRole> Users { get; } = new();

    public List<Company> Companies { get; } = [];
    
    public Tenant(string name, Guid? id = null) : base(id ?? Guid.CreateVersion7())
    {
        Name = name;
    }
    
    private Tenant() { }
}
