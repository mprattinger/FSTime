using ErrorOr;

namespace FSTime.Application.Users;

public static class UserErrors
{
    public static Error Already_Exists(string user) => Error.Conflict("USER_HANDLER.ALREADY_EXISTS", $"User {user} already exists!");
    public static Error Creation_Error(string user, string error) => Error.Conflict("USER_HANDLER.GEN_ERROR", $"When creating the user {user} an error occured: {error}");
}
