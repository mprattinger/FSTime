using System.Security.Cryptography;
using System.Text;
using FSTime.Application.Common.Interfaces;
using FSTime.Infrastructure.Common;
using Microsoft.AspNetCore.Identity;

namespace FSTime.Infrastructure.Services;

public class PasswordService(FSTimeSecuritySettings securitySettings) : IPasswordService
{
    // private readonly PasswordHasher<string> _passwordHasher = new PasswordHasher<string>();
    //
    // public string HashPassword(string password)
    // {
    //     return _passwordHasher.HashPassword("", password.Normalize()).Normalize();
    // }

    public bool VerifyPassword(string hashedPassword, string hashedSalt, string providedPassword)
    {
        // return _passwordHasher.VerifyHashedPassword("", hashedPassword, providedPassword);
        var pwdToCheck = HashPassword(providedPassword, hashedSalt);
        
        return pwdToCheck.password == hashedPassword;
    }

    public (string password, string salt) HashPassword(string password, string? salt = null)
    {
        if(string.IsNullOrEmpty(securitySettings.Pepper) || securitySettings.Iterations <= 0)
        {
            throw new InvalidOperationException("Pepper or Iterations not set");
        }
        
        var slt = salt ?? GenerateSalt();
        var pwd = password.Normalize();
        
        for (int i = 0; i <= securitySettings.Iterations; i++)
        {
            using var s = SHA512.Create();
            var passwordSaltPepper = $"{pwd}{slt}{securitySettings.Pepper}".Normalize();
            var byteValue = Encoding.UTF8.GetBytes(passwordSaltPepper);
            var byteHash = s.ComputeHash(byteValue);
            pwd = Convert.ToBase64String(byteHash).Normalize();    
        }

        return (pwd, slt);
    }
    
    public string GenerateSalt()
    {
        using var rng = RandomNumberGenerator.Create();
        var byteSalt = new byte[16];
        rng.GetBytes(byteSalt);
        var salt = Convert.ToBase64String(byteSalt);
        return salt;
    }
}
