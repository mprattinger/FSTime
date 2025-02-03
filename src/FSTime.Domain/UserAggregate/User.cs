using System;
using ErrorOr;
using FSTime.Domain.Common;

namespace FSTime.Domain.UserAggregate;

public class User : AggregateRoot
{
    private Guid? _employeeId;
    private readonly List<string> _roles = ["USER"];

    public User(Guid? id = null)
    : base(id ?? Guid.NewGuid())
    {
    }

    public ErrorOr<Success> AddRole(string role)
    {
        if (_roles.Contains(role))
        {
            return UserErrors.Already_has_role(role);
        }

        if (role != "USER" && role != "ADMIN" && role != "SUPERVISOR")
        {
            return UserErrors.Invalid_role(role);
        }

        _roles.Add(role);

        return Result.Success;
    }

    public ErrorOr<bool> HasRole(string role)
    {
        if (role != "USER" && role != "ADMIN" && role != "SUPERVISOR")
        {
            return UserErrors.Invalid_role(role);
        }

        return _roles.Contains(role);
    }

    public ErrorOr<Success> AssignUser(Guid employeeId)
    {
        _employeeId = employeeId;

        return Result.Success;
    }
}
