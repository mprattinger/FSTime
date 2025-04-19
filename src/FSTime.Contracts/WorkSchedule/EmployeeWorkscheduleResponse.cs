namespace FSTime.Contracts.WorkSchedule;

public class EmployeeWorkscheduleResponse
{
    public Guid WorkscheduleId { get; set; }
    public DateOnly From { get; set; }
    public DateOnly? To { get; set; }

    public string Description { get; set; } = "";

    public double? Monday { get; set; }
    public double? Tuesday { get; set; }
    public double? Wednesday { get; set; }
    public double? Thursday { get; set; }
    public double? Friday { get; set; }
    public double? Saturday { get; set; }
    public double? Sunday { get; set; }
}