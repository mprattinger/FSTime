using FSTime.Domain.Common;

namespace FSTime.Domain.UserAggregate;

public class User : AggregateRoot
{
    public string UserName { get; } = null!;

    public string Password { get; } = null!;
    
    public string Salt { get; } = null!;  
    
    public string Email { get; } = null!;

    public bool Verified { get; }

    public User(string name, string password, string email, string salt, Guid? id = null)
    : base(id ?? Guid.CreateVersion7())
    {
        UserName = name;
        Password = password;
        Email = email;
        Salt = salt;
        Verified = false;
    }

    private User() { }
}