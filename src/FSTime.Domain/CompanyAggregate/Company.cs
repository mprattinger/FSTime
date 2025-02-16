using FSTime.Domain.Common;

namespace FSTime.Domain.CompanyAggregate;

public class Company : AggregateRoot
{
    string _name;
    bool _licensed;

    public Company(string name, Guid? id) : base(id ?? Guid.NewGuid())
    {
        _name = name;
    }

    public bool IsLicensed() => _licensed;

    private Company() { }
}
