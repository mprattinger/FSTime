namespace FSTime.Contracts.Employees;

public class AddWorkscheduleRequest
{
    public Guid EmployeeId { get; set; }
    public Guid WorkscheduleId { get; set; }
    public DateOnly ValidFrom { get; set; }
}