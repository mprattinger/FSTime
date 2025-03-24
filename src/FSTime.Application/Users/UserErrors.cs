using ErrorOr;

namespace FSTime.Application.Users;

public static class UserErrors
{
    public static Error Already_Exists(string user) => Error.Conflict("USER_HANDLER.ALREADY_EXISTS", $"User {user} already exists!");
    public static Error Creation_Error(string user, string error) => Error.Conflict("USER_HANDLER.GEN_ERROR", $"When creating the user {user} an error occured: {error}");
    public static Error User_Not_Found => Error.NotFound("USER_HANDLER.NOT_FOUND", "User not found!");
    public static Error Verification_Token_Expired => Error.Forbidden("USER_HANDLER.VERIFICATION_TOKEN_EXPIRED", "The verification token has expired!");
}
