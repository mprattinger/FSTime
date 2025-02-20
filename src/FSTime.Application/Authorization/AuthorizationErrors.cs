using ErrorOr;

namespace FSTime.Application.Authorization;

public static class AuthorizationErrors
{
    public static Error Login_Not_Valid() => Error.Forbidden("USER_LOGIN.NOT_VALID", "User or password not valid!");
    public static Error User_Not_Verified() => Error.Conflict("USER_LOGIN.NOT_VERIFIED", "User not verified!");
}
