using FSTime.Domain.Common;

namespace FSTime.Domain.UserAggregate;

public class User : AggregateRoot
{
    private bool _verified;

    public string UserName { get; } = null!;

    public string Password { get; } = null!;

    public string Email { get; } = null!;

    public User(string name, string password, string email, Guid? id = null)
    : base(id ?? Guid.NewGuid())
    {
        UserName = name;
        Password = password;
        Email = email;
        _verified = false;
    }

    private User() { }
}
