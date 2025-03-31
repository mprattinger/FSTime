namespace FSTime.Contracts.Employees;

public record CreateEmployeeRequest(
    string FirstName,
    string LastName,
    string? MiddleName = "",
    DateTime? entryDate = null,
    Guid? supervisorId = null,
    bool? isHead = null);