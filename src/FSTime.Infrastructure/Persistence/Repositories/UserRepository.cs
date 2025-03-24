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
    
    public async Task<User> UpdateUser(User user)
    {
        context.Users.Update(user);
        await context.SaveChangesAsync();
        return user;
    }
    
    public async Task<User?> GetUser(string username)
    {
        return await context.Users.FirstOrDefaultAsync(x => x.UserName == username);
    }

    public async Task<User?> GetUserById(Guid id)
    {
        return await context.Users.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<bool> UserExists(string username)
    {
        return await context.Users.AnyAsync(u => u.UserName == username);
    }

    public async Task<User?> GetUserByVerificationData(string email, string token)
    {
        return await context.Users.FirstOrDefaultAsync(x => x.Email == email && x.VerifyToken == token);
    }
}
