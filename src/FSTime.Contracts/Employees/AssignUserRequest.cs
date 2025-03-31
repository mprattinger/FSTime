namespace FSTime.Contracts.Employees;

public record AssignUserRequest(Guid EmployeeId, Guid UserId);