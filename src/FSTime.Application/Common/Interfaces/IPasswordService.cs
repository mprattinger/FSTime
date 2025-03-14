using Microsoft.AspNetCore.Identity;

namespace FSTime.Application.Common.Interfaces;

public interface IPasswordService
{
    (string password, string salt) HashPassword(string password, string? salt = null);
    bool VerifyPassword(string hashedPassword, string hashedSalt,string providedPassword);
}
