using FSTime.Application.Common.Interfaces;
using FSTime.Domain.UserAggregate;
using Microsoft.EntityFrameworkCore;

namespace FSTime.Infrastructure.Persistence.Repositories;

public class UserRepository(FSTimeDbContext context) : IUserRepository
{
    public async Task<User> AddUser(User user)
    {
        await context.Users.AddAsync(user);
        await context.SaveChangesAsync();
        return user;
    }

    public async Task<bool> UserExists(string username)
    {
        return await context.Users.AnyAsync(u => u.UserName == username);
    }
}
