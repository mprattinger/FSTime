namespace FSTime.Contracts.Employees;

public record CreateEmployeeRequest(string FirstName, string LastName, string? MiddleName = "");