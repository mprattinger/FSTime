using FluentAssertions;
using FSTime.Domain.Tests.TestUtils;
using FSTime.Domain.UserAggregate;

namespace FSTime.Domain.Tests;

public class UserTests
{
    [Fact]
    public void AssignEmployee_Should_Work()
    {
        var u = UserFactory.CreateUser();

        u.AssignUser(Guid.NewGuid()).IsError.Should().BeFalse();
    }

    [Fact]
    public void AssignRole_Admin_ToUser_Should_Work()
    {
        var u = UserFactory.CreateUser();

        u.AddRole("ADMIN").IsError.Should().BeFalse();
    }

    [Fact]
    public void AssignRole_Supervisor_ToUser_Should_Work()
    {
        var u = UserFactory.CreateUser();

        u.AddRole("SUPERVISOR").IsError.Should().BeFalse();
    }

    [Fact]
    public void AssignRole_User_ToUser_Should_Fail()
    {
        var u = UserFactory.CreateUser();

        var result = u.AddRole("USER");

        result.IsError.Should().BeTrue();
        result.FirstError.Should().Be(UserErrors.Already_has_role("USER"));
    }

    [Fact]
    public void AssignRole_UNKNOW_ToUSER_Should_Fail()
    {
        var u = UserFactory.CreateUser();

        var result = u.AddRole("UNKNOWN");

        result.IsError.Should().BeTrue();
        result.FirstError.Should().Be(UserErrors.Invalid_role("UNKNOWN"));
    }

    [Fact]
    public void AssignRole_Admin_ToSUPERVISOR_Should_Work()
    {
        var u = UserFactory.CreateSupervisor();

        u.AddRole("ADMIN").IsError.Should().BeFalse();
    }

    [Fact]
    public void AssignRole_User_ToSUPERVISOR_Should_Fail()
    {
        var u = UserFactory.CreateSupervisor();

        var result = u.AddRole("USER");

        result.IsError.Should().BeTrue();
        result.FirstError.Should().Be(UserErrors.Already_has_role("USER"));
    }

    [Fact]
    public void AssignRole_SUPERVISOR_ToSUPERVISOR_Should_Fail()
    {
        var u = UserFactory.CreateSupervisor();

        var result = u.AddRole("SUPERVISOR");

        result.IsError.Should().BeTrue();
        result.FirstError.Should().Be(UserErrors.Already_has_role("SUPERVISOR"));
    }

    [Fact]
    public void AssignRole_UNKNOW_ToSUPERVISOR_Should_Fail()
    {
        var u = UserFactory.CreateSupervisor();

        var result = u.AddRole("UNKNOWN");

        result.IsError.Should().BeTrue();
        result.FirstError.Should().Be(UserErrors.Invalid_role("UNKNOWN"));
    }

    [Fact]
    public void AssignRole_Supervisor_ToADMIN_Should_Work()
    {
        var u = UserFactory.CreateAdmin();

        u.AddRole("SUPERVISOR").IsError.Should().BeFalse();
    }

    [Fact]
    public void AssignRole_User_ToADMIN_Should_Fail()
    {
        var u = UserFactory.CreateAdmin();

        var result = u.AddRole("USER");

        result.IsError.Should().BeTrue();
        result.FirstError.Should().Be(UserErrors.Already_has_role("USER"));
    }

    [Fact]
    public void AssignRole_Admin_ToADMIN_Should_Fail()
    {
        var u = UserFactory.CreateAdmin();

        var result = u.AddRole("ADMIN");

        result.IsError.Should().BeTrue();
        result.FirstError.Should().Be(UserErrors.Already_has_role("ADMIN"));
    }

    [Fact]
    public void AssignRole_UNKNOW_ToADMIN_Should_Fail()
    {
        var u = UserFactory.CreateAdmin();

        var result = u.AddRole("UNKNOWN");

        result.IsError.Should().BeTrue();
        result.FirstError.Should().Be(UserErrors.Invalid_role("UNKNOWN"));
    }
}
