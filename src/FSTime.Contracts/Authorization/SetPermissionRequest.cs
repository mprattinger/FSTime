namespace FSTime.Contracts.Authorization;

public record SetPermissionRequest(Guid UserId, string Group, string Action);
