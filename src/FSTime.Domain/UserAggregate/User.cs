using FSTime.Domain.Common;
using FSTime.Domain.EmployeeAggregate;

namespace FSTime.Domain.UserAggregate;

public class User : AggregateRoot
{
    public string UserName { get; } = null!;

    public string Password { get; } = null!;
    
    public string Salt { get; } = null!;  
    
    public string Email { get; } = null!;

    public bool Verified { get; private set; }

    public string VerifyToken { get; } = null!;

    public DateTime VerifyTokenExpires { get; }

    public Guid? EmployeeId { get; }
    public Employee? Employee { get; }
    
    public User(string name, string password, string email, string salt, string verifyToken, DateTime verifyExpires, Guid? id = null)
    : base(id ?? Guid.CreateVersion7())
    {
        UserName = name;
        Password = password;
        Email = email;
        Salt = salt;
        Verified = false;
        VerifyToken = verifyToken;
        VerifyTokenExpires = verifyExpires;
    }
    
    public void SetVerified()
    {
        Verified = true;
    }

    private User() { }
}