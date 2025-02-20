using FSTime.Domain.Common;

namespace FSTime.Domain.TenantAggregate;

public class Tenant : AggregateRoot
{
    public string Name { get; set; } = "";

    public bool IsLicensed { get; set; }
    
    public Tenant(string name, Guid? id) : base(id ?? Guid.CreateVersion7())
    {
        Name = name;
    }

    private Tenant() { }
}
