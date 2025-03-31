using FSTime.Contracts.Employees;

namespace FSTime.Contracts.Users;

public class UserResponse
{
    public Guid Id { get; set; }
    public string UserName { get; set; } = "";
    public string Email { get; set; } = "";
    public bool Verified { get; set; }
    public EmployeeResponse Emplyoee { get; set; } = null!;
}