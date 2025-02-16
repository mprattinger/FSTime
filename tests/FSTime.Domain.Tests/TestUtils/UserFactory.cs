using FSTime.Domain.UserAggregate;

namespace FSTime.Domain.Tests.TestUtils;

public class UserFactory
{
    public static User CreateUser()
    {
        var u = new User("test", "test", "test@test.com");

        return u;
    }

    public static User CreateAdmin()
    {
        var u = new User("test", "test", "test@test.com");

        return u;
    }

    public static User CreateSupervisor()
    {
        var u = new User("test", "test", "test@test.com");

        return u;
    }
}
