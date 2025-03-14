using FSTime.Infrastructure.Common;
using FSTime.Infrastructure.Services;
using Shouldly;

namespace InfrastructureTests.Auth;

public class PasswordServiceTests
{
    [Fact]
    public void PasswordCheckShouldBeTrue()
    {
        var sut = new PasswordService(new FSTimeSecuritySettings { Iterations = 3, Pepper = "pepper" });
        
        var pwdResult = sut.HashPassword("Password123");
        var checkResult = sut.VerifyPassword(pwdResult.password, pwdResult.salt, "Password123");
        
        checkResult.ShouldBeTrue();
    }
    
    [Fact]
    public void WrongPasswordCheckShouldBeFalse()
    {
        var sut = new PasswordService(new FSTimeSecuritySettings { Iterations = 3, Pepper = "pepper" });
        
        var pwdResult = sut.HashPassword("Password123");
        var checkResult = sut.VerifyPassword(pwdResult.password, pwdResult.salt, "Password135");
        
        checkResult.ShouldBeFalse();
    }
    
    [Fact]
    public void NotIterationsShouldThrow()
    {
        var sut = new PasswordService(new FSTimeSecuritySettings { Iterations = 0, Pepper = "pepper" });
        
        Should.Throw<InvalidOperationException>(() => sut.HashPassword("Password123"));
    }
    
    [Fact]
    public void NotPepperShouldThrow()
    {
        var sut = new PasswordService(new FSTimeSecuritySettings { Iterations = 3, Pepper = "" });
        
        Should.Throw<InvalidOperationException>(() => sut.HashPassword("Password123"));
    }
}