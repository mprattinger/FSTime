using FSTime.Contracts.Users;

namespace FSTime.Contracts.Employees;

public class EmployeeResponse
{
    public Guid Id { get; set; }

    public Guid CompanyId { get; set; }

    public string FirstName { get; set; } = "";

    public string LastName { get; set; } = "";

    public string? MiddleName { get; set; }

    public string? EmployeeCode { get; set; }

    public DateTime? EntryDate { get; set; }

    public Guid? WorkplanId { get; set; }

    public UserResponse? User { get; set; }

    public EmployeeResponse? Supervisor { get; set; }
    public bool IsHead { get; set; }
}