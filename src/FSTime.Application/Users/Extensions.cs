using FSTime.Contracts.Users;
using FSTime.Domain.UserAggregate;

namespace FSTime.Application.Users;

public static class Extensions
{
    public static UserResponse ToUserResponse(this User user)
    {
        return new UserResponse
        {
            Id = user.Id,
            UserName = user.UserName,
            Email = user.Email,
            Verified = user.Verified
        };
    }
}