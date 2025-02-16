
namespace FSTime.Domain.Common.ValueObjects;

public class Role : ValueObject
{
    private Guid _userId;
    private Guid _companyId;
    private string _roleName;

    public Role(Guid userId, Guid companyId, string roleName)
    {
        _userId = userId;
        _companyId = companyId;
        _roleName = roleName;
    }

    private Role() { }

    public override IEnumerable<object?> GetEqualityComponents()
    {
        yield return _userId;
        yield return _companyId;
        yield return _roleName;
    }
}
