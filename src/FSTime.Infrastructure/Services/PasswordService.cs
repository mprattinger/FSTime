using FSTime.Application.Common.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace FSTime.Infrastructure.Services;

public class PasswordService : IPasswordService
{
    private readonly PasswordHasher<string> _passwordHasher = new PasswordHasher<string>();

    public string HashPassword(string password)
    {
        return _passwordHasher.HashPassword("", password.Normalize()).Normalize();
    }

    public PasswordVerificationResult VerifyPassword(string hashedPassword, string providedPassword)
    {
        return _passwordHasher.VerifyHashedPassword("", hashedPassword, providedPassword);
    }
}
