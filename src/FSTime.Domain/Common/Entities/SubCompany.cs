namespace FSTime.Domain.Common.Entities;

public class SubCompany : Entity
{
    string _name;
    Guid _companyId;

    public SubCompany(string name, Guid companyId, Guid? id) : base(id ?? Guid.NewGuid())
    {
        _name = name;
        _companyId = companyId;
    }

    private SubCompany() { }
}
