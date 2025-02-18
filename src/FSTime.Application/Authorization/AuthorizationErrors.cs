using ErrorOr;

namespace FSTime.Application.Authorization;

public static class AuthorizationErrors
{
    public static Error Login_Not_Valid() => Error.Forbidden("USER_LOGIN.NOT_VALID", "User or password not valid!");
}
