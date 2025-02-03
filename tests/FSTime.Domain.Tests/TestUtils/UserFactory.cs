using System;
using FSTime.Domain.UserAggregate;

namespace FSTime.Domain.Tests.TestUtils;

public class UserFactory
{
    public static User CreateUser()
    {
        var u = new User();

        return u;
    }

    public static User CreateAdmin()
    {
        var u = new User();
        u.AddRole("ADMIN");

        return u;
    }

    public static User CreateSupervisor()
    {
        var u = new User();
        u.AddRole("SUPERVISOR");

        return u;
    }
}
