using FSTime.Domain.Common;

namespace FSTime.Domain.UserAggregate;

public class User : AggregateRoot
{
    public string UserName { get; } = null!;

    public string Password { get; } = null!;

    public string Email { get; } = null!;

    public bool Verified { get; }

    public User(string name, string password, string email, Guid? id = null)
    : base(id ?? Guid.CreateVersion7())
    {
        UserName = name;
        Password = password;
        Email = email;
        Verified = false;
    }

    private User() { }
}
