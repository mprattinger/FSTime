using ErrorOr;

namespace FSTime.Application.Authorization;

public static class AuthorizationErrors
{
    public static Error Login_Not_Valid()
    {
        return Error.Forbidden("USER_LOGIN.NOT_VALID", "User or password not valid!");
    }

    public static Error User_Not_Verified()
    {
        return Error.Conflict("USER_LOGIN.NOT_VERIFIED", "User not verified!");
    }

    public static Error NoPermissions()
    {
        return Error.NotFound("PERMISSION_HANDLER.MY.NOT_FOUND", "No permissions found for user!");
    }

    public static Error Permissions_GenError(string msg)
    {
        return Error.Conflict("PERMISSION_HANDLER.MY.ERROR", "When retrieving permissions an error occured: " + msg);
    }

    public static Error GetActions_InvalidGroup(string grp)
    {
        return Error.Validation("GETACTIONS_HANDLER.GROUP_NOT_VALID", $"Group {grp} is not valid");
    }
}