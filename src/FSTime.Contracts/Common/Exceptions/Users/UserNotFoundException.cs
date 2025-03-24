namespace FSTime.Contracts.Common.Exceptions.Users;

public class UserNotFoundException : Exception
{
    public UserNotFoundException(Guid userId): base($"Couldn't find user {userId}")
    {
        
    }
}