using FSTime.Domain.UserAggregate;

namespace FSTime.Application.Common.Interfaces;

public interface IUserRepository
{
    Task<User> AddUser(User user);
    Task<bool> UserExists(string username);
    Task<User?> GetUser(string username);
    Task<User?> GetUserById(Guid id);
}
