using FSTime.Domain.EmployeeAggregate;
using FSTime.Domain.WorkScheduleAggregate;

namespace FSTime.Domain.Common.ValueObjects;

public class EmployeeWorkschedule : ValueObject
{
    public EmployeeWorkschedule(Guid employeeId, Guid workplanId, DateTime from, DateTime? to = null)
    {
        EmployeeId = employeeId;
        WorkscheduleId = workplanId;
        From = from;
        To = to;
    }

    private EmployeeWorkschedule()
    {
    }

    public Guid EmployeeId { get; }
    public Employee Employee { get; } = null!;

    public Guid WorkscheduleId { get; }
    public WorkSchedule Workschedule { get; } = null!;

    public DateTime From { get; }
    public DateTime? To { get; }

    public override IEnumerable<object?> GetEqualityComponents()
    {
        yield return EmployeeId;
        yield return WorkscheduleId;
        yield return From;
    }
}