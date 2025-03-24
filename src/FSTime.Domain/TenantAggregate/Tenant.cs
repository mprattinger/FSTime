using FSTime.Domain.Common;
using FSTime.Domain.Common.ValueObjects;
using FSTime.Domain.CompanyAggregate;
using FSTime.Domain.UserAggregate;

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
    
    public void AddUser(TenantRole user)
    {
        Users.Add(user);
    }

    public void UpdateUser(TenantRole user)
    {
        Users.RemoveAll(x => x.UserId == user.UserId);
        Users.Add(user);
    }

    public void RemoveUser(Guid userId)
    {
        Users.RemoveAll(x => x.UserId == userId);
    }
    
    private Tenant() { }
}