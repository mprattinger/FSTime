using Microsoft.AspNetCore.Identity;

namespace FSTime.Application.Common.Interfaces;

public interface IPasswordService
{
    string HashPassword(string password);
    PasswordVerificationResult VerifyPassword(string hashedPassword, string providedPassword);
}
