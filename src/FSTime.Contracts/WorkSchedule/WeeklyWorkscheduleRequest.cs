namespace FSTime.Contracts.WorkSchedule;

public class WeeklyWorkscheduleRequest
{
    public string Description { get; set; } = "";
    public double WeeklyWorktime { get; set; }
    public int Workdays { get; set; }
}