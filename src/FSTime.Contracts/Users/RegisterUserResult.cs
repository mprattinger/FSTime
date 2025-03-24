namespace FSTime.Contracts.Users;

public record RegisterUserResult(Guid UserId, string VerifyToken, string Email);
